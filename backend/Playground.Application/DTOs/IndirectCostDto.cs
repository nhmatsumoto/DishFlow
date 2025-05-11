namespace Playground.Application.DTOs
{
    public class IndirectCostDto
    {
        public Guid IndirectCostId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PeriodType { get; set; }
        public string CategoryName { get; set; }
        public Guid RestaurantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
