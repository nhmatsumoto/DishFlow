using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Infrastructure.Context;

namespace Playground.Infrastructure.Repositories;

public class IndirectCostRepository : IIndirectCostRepository
{
    private readonly PlaygroundDbContext _context;

    public IndirectCostRepository(PlaygroundDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(IndirectCost indirectCost)
    {
        await _context.IndirectCosts.AddAsync(indirectCost);
        await _context.SaveChangesAsync();
    }

    public async Task<IndirectCost?> GetByIdAsync(Guid id)
    {
        return await _context.IndirectCosts.FindAsync(id);
    }

    public async Task UpdateAsync(IndirectCost indirectCost)
    {
        _context.IndirectCosts.Update(indirectCost);
        await _context.SaveChangesAsync();
    }
}