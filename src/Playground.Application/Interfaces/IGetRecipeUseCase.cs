using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface IGetRecipeUseCase
{
    Task<RecipeDto?> GetById(Guid id);
    Task<IEnumerable<RecipeDto>> GetAllAsync();
}
