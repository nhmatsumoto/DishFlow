using Playground.Domain.Common;
using Playground.Domain.ValueObjects;

namespace Playground.Domain.Entities;

/// <summary>
/// Representa a associação entre um prato e os ingredientes usados nele, com suas quantidades
/// </summary>

public class RecipeIngredient : BaseEntity
{
    public Guid RecipeIngredientId { get; init; } = Guid.NewGuid();
    public Guid RecipeId { get; private set; }
    public Guid IngredientId { get; private set; }
    public decimal QuantityUsed { get; private set; }
    public UnitOfMeasure QuantityUnit { get; private set; }
    public Money UnitCost { get; private set; }

    public Recipe Recipe { get; private set; }
    public Ingredient Ingredient { get; private set; }

    private RecipeIngredient() { }

    public RecipeIngredient(Ingredient ingredient, decimal quantityUsed, UnitOfMeasure quantityUnit)
    {
        IngredientId = ingredient?.IngredientId ?? throw new ArgumentNullException(nameof(ingredient));
        QuantityUsed = quantityUsed >= 0 ? quantityUsed : throw new ArgumentException("A quantidade usada deve ser não-negativa.");
        QuantityUnit = quantityUnit ?? throw new ArgumentNullException(nameof(quantityUnit));
        UnitCost = ingredient.UnitPrice ?? throw new ArgumentNullException(nameof(ingredient.UnitPrice));
    }

    public void UpdateQuantity(decimal quantityUsed, UnitOfMeasure quantityUnit)
    {
        if (quantityUsed < 0)
            throw new ArgumentException("A quantidade usada deve ser não-negativa.");
        QuantityUsed = quantityUsed;
        QuantityUnit = quantityUnit ?? throw new ArgumentNullException(nameof(quantityUnit));
    }
}