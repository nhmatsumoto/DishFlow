using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface ICreateRecipeUseCase
{
    Task<RecipeDto> ExecuteAsync(string name, decimal profitMargin, List<RecipeIngredientDto> ingredients, Guid restaurantId);
}
