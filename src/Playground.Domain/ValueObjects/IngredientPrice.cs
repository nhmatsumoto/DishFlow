namespace Playground.Domain.ValueObjects;

/// <summary>
/// Representa o preço de um ingrediente.
/// </summary>
public class IngredientPrice : ValueObject
{
    public decimal UnitPrice { get; }
    public DateTime PurchaseDate { get; }

    public IngredientPrice(decimal unitPrice, DateTime purchaseDate)
    {
        UnitPrice = unitPrice;
        PurchaseDate = purchaseDate;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UnitPrice;
        yield return PurchaseDate;
    }
}