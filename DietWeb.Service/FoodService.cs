

using DietWeb.Core.Models;
using DietWeb.Core.Repositories;
using DietWeb.Core.Services;

namespace DietWeb.Service
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _repository;

        public FoodService(IFoodRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Food>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Food?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<Food> AddAsync(Food food) => _repository.AddAsync(food);

        public Task UpdateAsync(Food food) => _repository.UpdateAsync(food);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }

}
