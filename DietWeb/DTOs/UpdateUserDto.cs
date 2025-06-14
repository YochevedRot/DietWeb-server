using DietWeb.Core.Models;

namespace DietWeb.API.DTOs
{
    public class UpdateUserDto
    {
        public float? StartWeight { get; set; }
        public int? Height { get; set; }
        public string? ChatPersonality { get; set; }
        public List<DietaryPreference>? DietaryPreferences { get; set; }
    }

}
