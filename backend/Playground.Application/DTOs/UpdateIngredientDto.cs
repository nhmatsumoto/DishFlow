namespace Playground.Application.DTOs;

public class UpdateIngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPriceAmount { get; set; }
    public string UnitPriceCurrency { get; set; }
    public string UnitSymbol { get; set; }
}
