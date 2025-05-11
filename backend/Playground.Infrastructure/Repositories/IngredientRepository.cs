using Microsoft.EntityFrameworkCore;
using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Infrastructure.Context;

namespace Playground.Infrastructure.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly PlaygroundDbContext _context;

    public IngredientRepository(PlaygroundDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(Ingredient ingredient)
    {
        await _context.Ingredients.AddAsync(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task<Ingredient?> GetByIdAsync(Guid id)
    {
        return await _context.Ingredients.FindAsync(id);
    }

    public async Task UpdateAsync(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Ingredient>> GetAllAsync()
    {
        return await _context.Ingredients.ToListAsync();
    }
}