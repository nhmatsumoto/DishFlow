using Playground.Domain.Common;
using Playground.Domain.ValueObjects;

namespace Playground.Domain.Entities;

public class IndirectCost : BaseEntity
{
    public Guid IndirectCostId { get; init; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Money Value { get; private set; }
    public Period Period { get; private set; }
    public CostCategory Category { get; private set; }
    public Restaurant Restaurant { get; set; }
    public Guid RestaurantId { get; private set; }

    private IndirectCost() { }

    public IndirectCost(string name, Money value, Period period, CostCategory category, Guid restaurantId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do custo não pode ser vazio.");
        if (restaurantId == Guid.Empty)
            throw new ArgumentException("O ID do restaurante não pode ser vazio.");
        Name = name;
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Period = period ?? throw new ArgumentNullException(nameof(period));
        Category = category ?? throw new ArgumentNullException(nameof(category));
        RestaurantId = restaurantId;
    }

    public Money GetMonthlyValue() => new Money(Period.ConvertToMonthly(Value.Amount), Value.Currency);

    public void UpdateValue(Money newValue)
    {
        Value = newValue ?? throw new ArgumentNullException(nameof(newValue));
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do custo não pode ser vazio.");
        Name = name;
    }

    public void UpdatePeriod(Period period)
    {
        Period = period ?? throw new ArgumentNullException(nameof(period));
    }

    public void UpdateCategory(CostCategory category)
    {
        Category = category ?? throw new ArgumentNullException(nameof(category));
    }
}