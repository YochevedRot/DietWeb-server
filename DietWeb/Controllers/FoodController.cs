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


    [HttpGet("search")]
    public async Task<IActionResult> SearchFoodByName([FromQuery] string foodName)
    {
        if (string.IsNullOrWhiteSpace(foodName))
        {
            // אם שם המאכל ריק או null, נחזיר שגיאת בקשה לא חוקית
            return BadRequest("Food name cannot be empty.");
        }

        // לוגיקת החיפוש:
        // נניח שלשירות ה-IFoodService יש מתודה בשם GetFoodByNameAsync
        // שיכולה לחפש מאכלים לפי שם.
        // המתודה הזו יכולה להחזיר:
        // 1. DietWeb.Core.Models.Food (אם מצפים למאכל בודד)
        // 2. IEnumerable<DietWeb.Core.Models.Food> או List<DietWeb.Core.Models.Food> (אם מצפים לרשימה של מאכלים תואמים)

        // אם מצפים למאכל בודד שמתאים לשם:
        // var food = await _foodService.GetFoodByNameAsync(foodName);
        // if (food == null)
        // {
        //     return NotFound($"Food '{foodName}' not found.");
        // }
        // return Ok(food);

        // או, אם מצפים לרשימה של מאכלים (וזה יותר סביר לפונקציית "חיפוש"):
        var foods = await _foodService.GetFoodByNameAsync(foodName); // כאן נניח שזו מתודה שמחזירה IEnumerable/List
        if (foods == null || !foods.Any()) // בדוק אם הרשימה ריקה או null
        {
            return NotFound($"No food found matching '{foodName}'.");
        }
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

