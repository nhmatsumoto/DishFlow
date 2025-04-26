using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Repository;

namespace Playground.Application.UseCases;

public class UpdateRestaurantUseCase : IUpdateRestaurantUseCase
{
    private readonly IRestaurantRepository _restaurantRepository;

    public UpdateRestaurantUseCase(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
    }

    public async Task<RestaurantDto> ExecuteAsync(Guid id, string name, int totalDishesSold)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null)
            throw new InvalidOperationException($"Restaurante com ID {id} não encontrado.");

        restaurant.UpdateName(name);
        restaurant.UpdateDishesSold(totalDishesSold);

        await _restaurantRepository.UpdateAsync(restaurant);

        return new RestaurantDto
        {
            RestaurantId = restaurant.RestaurantId,
            Name = restaurant.Name,
            TotalDishesSold = restaurant.TotalDishesSold,
            IndirectCosts = restaurant.IndirectCosts.Select(ic => new IndirectCostDto
            {
                IndirectCostId = ic.IndirectCostId,
                Name = ic.Name,
                Amount = ic.Value.Amount,
                Currency = ic.Value.Currency,
                PeriodType = ic.Period.Type.ToString(),
                CategoryName = ic.Category.Name,
                RestaurantId = ic.RestaurantId,
                CreatedAt = ic.CreatedAt,
                UpdatedAt = ic.UpdatedAt
            }).ToList(),
            Recipes = restaurant.Recipes.Select(r => new RecipeDto
            {
                RecipeId = r.RecipeId,
                Name = r.Name,
                ProfitMargin = r.ProfitMargin.Value,
                Ingredients = r.Ingredients.Select(ri => new RecipeIngredientDto
                {
                    RecipeIngredientId = ri.RecipeIngredientId,
                    IngredientId = ri.IngredientId,
                    QuantityUsed = ri.QuantityUsed,
                    QuantityUnitSymbol = ri.QuantityUnit.Symbol,
                    UnitCostAmount = ri.UnitCost.Amount,
                    UnitCostCurrency = ri.UnitCost.Currency
                }).ToList(),
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList(),
            CreatedAt = restaurant.CreatedAt,
            UpdatedAt = restaurant.UpdatedAt
        };
    }
}
