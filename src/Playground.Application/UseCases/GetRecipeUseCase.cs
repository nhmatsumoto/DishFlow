using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Entities;
using Playground.Domain.Repository;

namespace Playground.Application.UseCases;

public class GetRecipeUseCase : IGetRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;

    public GetRecipeUseCase(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
    }

    public async Task<RecipeDto?> GetById(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if (recipe == null)
            return null;

        return new RecipeDto
        {
            RecipeId = recipe.RecipeId,
            Name = recipe.Name,
            ProfitMargin = recipe.ProfitMargin.Value,
            Ingredients = recipe.Ingredients.Select(ri => new RecipeIngredientDto
            {
                RecipeIngredientId = ri.RecipeIngredientId,
                IngredientId = ri.IngredientId,
                QuantityUsed = ri.QuantityUsed,
                QuantityUnitSymbol = ri.QuantityUnit.Symbol,
                UnitCostAmount = ri.UnitCost.Amount,
                UnitCostCurrency = ri.UnitCost.Currency
            }).ToList(),
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        };
    }

    public async Task<IEnumerable<RecipeDto>> GetAllAsync()
    {
        var recipes = await _recipeRepository.GetAllAsync();
        return recipes.Select(MapToDto).ToList();
    }

    private RecipeDto MapToDto(Recipe recipe)
    {
        return new RecipeDto
        {
            RecipeId = recipe.RecipeId,
            Name = recipe.Name,
            ProfitMargin = recipe.ProfitMargin.Value,
            RestaurantId = recipe.RestaurantId,
            Ingredients = recipe.Ingredients.Select(ri => new RecipeIngredientDto
            {
                RecipeIngredientId = ri.RecipeIngredientId,
                IngredientId = ri.IngredientId,
                QuantityUsed = ri.QuantityUsed,
                QuantityUnitSymbol = ri.QuantityUnit?.Symbol ?? "un", // Usa o Symbol diretamente
                UnitCostAmount = ri.UnitCost.Amount,
                UnitCostCurrency = ri.UnitCost.Currency
            }).ToList(),
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        };
    }
}
