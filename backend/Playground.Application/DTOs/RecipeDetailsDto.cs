namespace Playground.Application.DTOs;

public class RecipeDetailsDto
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; } = "";
    public List<RecipeIngredientDto> Ingredients { get; set; } = new();
    public decimal LaborCost { get; set; }
    public decimal EnergyCost { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal ProfitMargin { get; set; }

    public decimal TotalCost { get; set; }
    public decimal SellingPrice { get; set; }
}
