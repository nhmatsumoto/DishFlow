using Playground.Domain.Common;
using Playground.Domain.ValueObjects;

namespace Playground.Domain.Entities;

/// <summary>
/// Representa uma receita, que é um prato que pode ser preparado com ingredientes
/// </summary>
public class Recipe : BaseEntity
{
    public Guid RecipeId { get; init; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Percentage ProfitMargin { get; private set; }
    private readonly List<RecipeIngredient> _ingredients = new();
    public IReadOnlyCollection<RecipeIngredient> Ingredients => _ingredients.AsReadOnly();

    public Restaurant Restaurant { get; private set; }
    public Guid RestaurantId { get; private set; }

    private Recipe() { }

    public Recipe(string name, Percentage profitMargin, Guid restaurantId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome da receita não pode ser vazio.");
        if (restaurantId == Guid.Empty)
            throw new ArgumentException("O ID do restaurante não pode ser vazio.");
        Name = name;
        ProfitMargin = profitMargin ?? throw new ArgumentNullException(nameof(profitMargin));
        RestaurantId = restaurantId;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome da receita não pode ser vazio.");
        Name = name;
    }

    public void UpdateProfitMargin(Percentage profitMargin)
    {
        ProfitMargin = profitMargin ?? throw new ArgumentNullException(nameof(profitMargin));
    }

    public void AddIngredient(Ingredient ingredient, decimal quantityUsed, UnitOfMeasure unit)
    {
        var recipeIngredient = new RecipeIngredient(ingredient, quantityUsed, unit);
        _ingredients.Add(recipeIngredient);
    }

    public void ClearIngredients()
    {
        _ingredients.Clear();
    }

    public Money CalculateDirectCost()
    {
        var totalAmount = _ingredients.Sum(ri => ri.QuantityUsed * ri.UnitCost.Amount);
        var currency = _ingredients.FirstOrDefault()?.UnitCost.Currency ?? "BRL";
        return new Money(totalAmount, currency);
    }
}