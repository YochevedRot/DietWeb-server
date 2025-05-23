using DietWeb.Core.Repositories;
using DietWeb.Core.Services;
using DietWeb.Data;
using DietWeb.Data.Repositories;
using DietWeb.Service;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// *** ���� �� ��: ����� ������� CORS ***
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()//WithOrigins("http://localhost:3000", "https://localhost:5173", "http://localhost:5173") // *** ��� ������ �� ������� ���! ***
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});



builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddDbContext<DataContext>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// *** ���� ��� ���� ������ ��� ***
app.UseRouting(); // <-- ���� ���� ���!
app.UseCors("AllowAnyOrigin"); // <-- ���� ���� ���!
app.UseAuthorization();
app.MapControllers();
// ******************************

app.Run();




