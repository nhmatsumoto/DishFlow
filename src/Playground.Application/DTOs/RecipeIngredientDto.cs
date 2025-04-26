namespace Playground.Application.DTOs;

public class RecipeIngredientDto
{
    public Guid RecipeIngredientId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal QuantityUsed { get; set; }
    public string QuantityUnitSymbol { get; set; }
    public decimal UnitCostAmount { get; set; }
    public string UnitCostCurrency { get; set; } = "BRL";
}
