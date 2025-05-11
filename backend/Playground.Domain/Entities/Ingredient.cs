using Playground.Domain.Common;
using Playground.Domain.ValueObjects;

namespace Playground.Domain.Entities;

/// <summary>
/// Representa um ingrediente
/// </summary>
public class Ingredient : BaseEntity
{
    public Guid IngredientId { get; init; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Money UnitPrice { get; private set; }
    public UnitOfMeasure Unit { get; private set; }

    private Ingredient() { }

    public Ingredient(string name, Money unitPrice, UnitOfMeasure unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do ingrediente não pode ser vazio.");
        Name = name;
        UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
    }

    public void UpdatePrice(Money newPrice)
    {
        UnitPrice = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do ingrediente não pode ser vazio.");
        Name = name;
    }

    public void UpdateUnit(UnitOfMeasure unit)
    {
        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
    }
}