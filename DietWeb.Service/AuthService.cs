// Service/AuthService.cs
using DietWeb.Core.Models;
using DietWeb.Core.Repositories;
using DietWeb.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DietWeb.API.DTOs;


namespace DietWeb.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository; // נשתמש ב-UserRepository הקיים

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        // שיטה לרישום משתמש חדש
        public async Task<User> Register(User user, string password)
        {
            // גיבוב הסיסמה לפני השמירה במסד הנתונים
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            // אם הייתם רוצים לאחסן מלח בנפרד:
            // user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            // user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);

            var createdUser = await _userRepository.AddAsync(user);
            return createdUser;
        }

        // שיטה לאימות פרטי התחברות
        public async Task<User> ValidateUserCredentials(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username); // נניח שקיימת מתודה כזו ב-UserRepository
            if (user == null)
            {
                return null; // משתמש לא נמצא
            }

            // אימות הסיסמה המגובבת
            if (!BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
            {
                return null; // סיסמה שגויה
            }

            return user;
        }

        // שיטה ליצירת JWT
        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // מזהה ייחודי של המשתמש
                new Claim(ClaimTypes.Name, user.Username), // שם המשתמש
                // ניתן להוסיף Claims נוספים כמו תפקידים (roles) במידה ויש:
                // new Claim(ClaimTypes.Role, "Admin"),
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"] ?? "60")), // זמן תפוגה, ברירת מחדל 60 דקות
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse?> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.HashedPassword);
            if (!isValid)
                return null;

            // Optional token generation
            string token = "fake-jwt-token"; // Replace with real token generation if needed

            return new LoginResponse
            {
                UserId = user.Id,
                Token = token
            };
        }
    }
}