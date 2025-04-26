namespace Playground.Application.DTOs;

public class UpdateRestaurantDto
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; }
    public int TotalDishesSold { get; set; }
}
