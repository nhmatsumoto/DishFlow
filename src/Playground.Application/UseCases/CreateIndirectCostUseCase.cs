using Playground.Application.Interfaces;
using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Domain.ValueObjects;

namespace Playground.Application.UseCases;

public class CreateIndirectCostUseCase : ICreateIndirectCostUseCase
{
    private readonly IIndirectCostRepository _indirectCostRepository;
    private readonly IRestaurantRepository _restaurantRepository;

    public CreateIndirectCostUseCase(
        IIndirectCostRepository indirectCostRepository,
        IRestaurantRepository restaurantRepository)
    {
        _indirectCostRepository = indirectCostRepository ?? throw new ArgumentNullException(nameof(indirectCostRepository));
        _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
    }

    public async Task ExecuteAsync(string name, decimal amount, string currency, string periodType, string categoryName, Guid restaurantId)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
            throw new InvalidOperationException($"Restaurante com ID {restaurantId} não encontrado.");

        var indirectCost = new IndirectCost(
            name: name,
            value: new Money(amount, currency),
            period: new Period(Enum.Parse<Period.PeriodType>(periodType)),
            category: new CostCategory(categoryName),
            restaurantId: restaurant.RestaurantId);

        await _indirectCostRepository.AddAsync(indirectCost);
    }
}
