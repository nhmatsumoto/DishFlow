using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Domain.ValueObjects;

namespace Playground.Application.UseCases;

public class CreateRecipeUseCase : ICreateRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IRestaurantRepository _restaurantRepository;

    public CreateRecipeUseCase(
        IRecipeRepository recipeRepository,
        IIngredientRepository ingredientRepository,
        IRestaurantRepository restaurantRepository)
    {
        _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
    }

    public async Task<RecipeDto> ExecuteAsync(string name, decimal profitMargin, List<RecipeIngredientDto> ingredients, Guid restaurantId)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
            throw new InvalidOperationException($"Restaurante com ID {restaurantId} não encontrado.");

        var recipe = new Recipe(name, new Percentage(profitMargin), restaurantId);

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

        await _recipeRepository.AddAsync(recipe);

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