using DietWeb.Core.Models;
using DietWeb.Core.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private readonly IFoodService _foodService;

    public FoodController(IFoodService service)
    {
        _foodService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var foods = await _foodService.GetAllAsync();
        return Ok(foods);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var food = await _foodService.GetByIdAsync(id);
        return food == null ? NotFound() : Ok(food);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Food food)
    {
        var created = await _foodService.AddAsync(food);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Food food)
    {
        if (id != food.Id) return BadRequest();
        await _foodService.UpdateAsync(food);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _foodService.DeleteAsync(id);
        return NoContent();
    }
}
