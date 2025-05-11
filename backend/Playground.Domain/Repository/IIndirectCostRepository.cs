using Playground.Domain.Entities;

namespace Playground.Domain.Repository;

public interface IIndirectCostRepository
{
    Task AddAsync(IndirectCost indirectCost);
    Task<IndirectCost?> GetByIdAsync(Guid id);
    Task UpdateAsync(IndirectCost indirectCost);
}
