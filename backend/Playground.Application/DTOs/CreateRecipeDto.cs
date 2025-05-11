namespace Playground.Application.DTOs;

public class CreateRecipeDto
{
    public string Name { get; set; }
    public decimal ProfitMargin { get; set; }
    public List<RecipeIngredientDto> Ingredients { get; set; }
    public Guid RestaurantId { get; set; }
}
