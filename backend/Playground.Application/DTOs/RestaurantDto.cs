namespace Playground.Application.DTOs;

public class RestaurantDto
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; }
    public int TotalDishesSold { get; set; }
    public List<IndirectCostDto> IndirectCosts { get; set; }
    public List<RecipeDto> Recipes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
