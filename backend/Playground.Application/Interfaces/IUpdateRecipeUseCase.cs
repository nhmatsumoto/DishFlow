using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface IUpdateRecipeUseCase
{
    Task<RecipeDto> ExecuteAsync(Guid id, string name, decimal profitMargin, List<RecipeIngredientDto> ingredients);
}