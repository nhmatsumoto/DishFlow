using Playground.Domain.Entities;

namespace Playground.Domain.Services;

public interface ICostCalculator
{
    Task<decimal> CalculateAsync(List<RecipeIngredient> ingredients);
}