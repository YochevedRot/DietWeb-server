using DietWeb.Core.Models;
using DietWeb.Core.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PersonalTrainerController : ControllerBase
{
    private readonly IPersonalTrainerService _trainerService;

    public PersonalTrainerController(IPersonalTrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskTrainer([FromBody] TrainerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Personality) ||
            string.IsNullOrWhiteSpace(request.Input) ||
            string.IsNullOrWhiteSpace(request.UserId))
        {
            return BadRequest("Missing required fields.");
        }

        var response = await _trainerService.GetTrainerResponseAsync(
            request.Personality, request.Input, request.UserId);

        return Ok(new { answer = response });
    }

}

