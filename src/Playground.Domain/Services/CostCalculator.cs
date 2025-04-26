using Playground.Domain.Entities;
using Playground.Domain.ValueObjects;

namespace Playground.Domain.Services;

public class CostCalculator
{
    public Money CalculateTotalIndirectCosts(Restaurant restaurant)
    {
        if (restaurant == null)
            throw new ArgumentNullException(nameof(restaurant));

        return restaurant.IndirectCosts
            .Aggregate(new Money(0, "BRL"), (sum, cost) => sum.Add(cost.GetMonthlyValue()));
    }

    public Money CalculateIndirectCostPerDish(Restaurant restaurant)
    {
        if (restaurant == null)
            throw new ArgumentNullException(nameof(restaurant));
        if (restaurant.TotalDishesSold == 0)
            throw new InvalidOperationException("O número de pratos vendidos não pode ser zero.");

        var totalCosts = CalculateTotalIndirectCosts(restaurant);
        return totalCosts.Divide(restaurant.TotalDishesSold);
    }

    public Money CalculateDirectCost(Recipe recipe)
    {
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));

        return recipe.CalculateDirectCost();
    }

    public Money CalculateTotalCostPerDish(Restaurant restaurant, Recipe recipe)
    {
        var directCost = CalculateDirectCost(recipe);
        var indirectCost = CalculateIndirectCostPerDish(restaurant);
        return directCost.Add(indirectCost);
    }

    public Money CalculateSellingPrice(Restaurant restaurant, Recipe recipe)
    {
        var totalCost = CalculateTotalCostPerDish(restaurant, recipe);
        return new Money(recipe.ProfitMargin.Apply(totalCost.Amount), totalCost.Currency);
    }
}
