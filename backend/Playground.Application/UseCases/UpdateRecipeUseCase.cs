using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Repository;
using Playground.Domain.ValueObjects;

namespace Playground.Application.UseCases;

public class UpdateRecipeUseCase : IUpdateRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public UpdateRecipeUseCase(
        IRecipeRepository recipeRepository,
        IIngredientRepository ingredientRepository)
    {
        _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
    }

    public async Task<RecipeDto> ExecuteAsync(Guid id, string name, decimal profitMargin, List<RecipeIngredientDto> ingredients)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if (recipe == null)
            throw new InvalidOperationException($"Receita com ID {id} não encontrada.");

        recipe.UpdateName(name);
        recipe.UpdateProfitMargin(new Percentage(profitMargin));
        recipe.ClearIngredients();

        foreach (var ingredientDto in ingredients)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(ingredientDto.IngredientId);
            if (ingredient == null)
                throw new InvalidOperationException($"Ingrediente com ID {ingredientDto.IngredientId} não encontrado.");

            recipe.AddIngredient(
                ingredient,
                ingredientDto.QuantityUsed,
                UnitOfMeasure.FromSymbol(ingredientDto.QuantityUnitSymbol));
        }

        await _recipeRepository.UpdateAsync(recipe);

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
}