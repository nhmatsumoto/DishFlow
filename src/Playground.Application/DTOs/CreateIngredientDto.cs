namespace Playground.Application.DTOs;

public class CreateIngredientDto
{
    public string Name { get; set; }
    public decimal UnitPriceAmount { get; set; }
    public string UnitPriceCurrency { get; set; }
    public string UnitSymbol { get; set; }
}
