namespace Playground.Application.DTOs;

public class RecipeDto
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public decimal ProfitMargin { get; set; }
    public List<RecipeIngredientDto> Ingredients { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid RestaurantId { get; set; }
}
