using DietWeb.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string? Email { get; set; }
    public string ? Phone { get; set; }
    public int? Gender  { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? ProgramLevel { get; set; }
    public float? StartWeight { get; set; }
    public float? GoalWeight { get; set; }
    public DateTime? GoalDate { get; set; }
    public DateTime? StartDate { get; set; }
    public WeightTracing? weightTracing { get; set; }
    public DietaryPreference? dietaryPreference { get; set; }
    public string? ChatPersonality { get; set; }
}