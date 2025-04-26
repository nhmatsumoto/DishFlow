namespace Playground.Application.DTOs;

public class RecipeCostDto
{
    public Guid RecipeId { get; set; }
    public decimal DirectCost { get; set; }
    public decimal IndirectCost { get; set; }
    public decimal TotalCost { get; set; }
    public decimal SellingPrice { get; set; }
}
