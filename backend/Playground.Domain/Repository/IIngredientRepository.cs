using Playground.Domain.Entities;

namespace Playground.Domain.Repository;

public interface IIngredientRepository
{
    Task AddAsync(Ingredient ingredient);
    Task<Ingredient?> GetByIdAsync(Guid id);
    Task UpdateAsync(Ingredient ingredient);
    Task<IEnumerable<Ingredient>> GetAllAsync();
}
