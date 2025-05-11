namespace Playground.Application.DTOs;

public class CreateIndirectCostDto
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string PeriodType { get; set; }
    public string CategoryName { get; set; }
    public Guid RestaurantId { get; set; }
}
