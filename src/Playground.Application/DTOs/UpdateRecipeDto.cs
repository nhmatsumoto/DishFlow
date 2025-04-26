namespace Playground.Application.DTOs;

public class UpdateRecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal ProfitMargin { get; set; }
    public List<RecipeIngredientDto> Ingredients { get; set; }
}
