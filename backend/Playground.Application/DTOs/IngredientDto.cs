namespace Playground.Application.DTOs;

public class IngredientDto
{
    public Guid IngredientId { get; set; }
    public string Name { get; set; }
    public decimal UnitPriceAmount { get; set; }
    public string UnitPriceCurrency { get; set; }
    public string UnitSymbol { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
