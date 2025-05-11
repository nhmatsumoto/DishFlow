using Playground.Application.Interfaces;
using Playground.Domain.Repository;
using Playground.Domain.ValueObjects;

namespace Playground.Application.UseCases;

public class UpdateIndirectCostUseCase : IUpdateIndirectCostUseCase
{
    private readonly IIndirectCostRepository _indirectCostRepository;

    public UpdateIndirectCostUseCase(IIndirectCostRepository indirectCostRepository)
    {
        _indirectCostRepository = indirectCostRepository ?? throw new ArgumentNullException(nameof(indirectCostRepository));
    }

    public async Task ExecuteAsync(Guid id, string name, decimal amount, string currency, string periodType, string categoryName)
    {
        var indirectCost = await _indirectCostRepository.GetByIdAsync(id);
        if (indirectCost == null)
            throw new InvalidOperationException($"Custo indireto com ID {id} não encontrado.");

        indirectCost.UpdateName(name);
        indirectCost.UpdateValue(new Money(amount, currency));
        indirectCost.UpdatePeriod(new Period(Enum.Parse<Period.PeriodType>(periodType)));
        indirectCost.UpdateCategory(new CostCategory(categoryName));

        await _indirectCostRepository.UpdateAsync(indirectCost);
    }
}
