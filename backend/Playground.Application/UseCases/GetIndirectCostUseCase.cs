using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Repository;

namespace Playground.Application.UseCases;

public class GetIndirectCostUseCase : IGetIndirectCostUseCase
{
    private readonly IIndirectCostRepository _indirectCostRepository;

    public GetIndirectCostUseCase(IIndirectCostRepository indirectCostRepository)
    {
        _indirectCostRepository = indirectCostRepository ?? throw new ArgumentNullException(nameof(indirectCostRepository));
    }

    public async Task<IndirectCostDto?> ExecuteAsync(Guid id)
    {
        var indirectCost = await _indirectCostRepository.GetByIdAsync(id);
        if (indirectCost == null)
            return null;

        return new IndirectCostDto
        {
            IndirectCostId = indirectCost.IndirectCostId,
            Name = indirectCost.Name,
            Amount = indirectCost.Value.Amount,
            Currency = indirectCost.Value.Currency,
            PeriodType = indirectCost.Period.Type.ToString(),
            CategoryName = indirectCost.Category.Name,
            RestaurantId = indirectCost.RestaurantId,
            CreatedAt = indirectCost.CreatedAt,
            UpdatedAt = indirectCost.UpdatedAt
        };
    }
}
