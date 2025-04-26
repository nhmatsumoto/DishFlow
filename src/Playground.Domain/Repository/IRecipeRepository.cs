using Playground.Domain.Entities;

namespace Playground.Domain.Repository;

public interface IRecipeRepository
{
    Task AddAsync(Recipe recipe);
    Task<Recipe?> GetByIdAsync(Guid id);
    Task UpdateAsync(Recipe recipe);
    Task<IEnumerable<Recipe>> GetAllAsync();
}