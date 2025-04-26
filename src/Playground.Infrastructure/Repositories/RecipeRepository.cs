using Microsoft.EntityFrameworkCore;
using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Infrastructure.Context;

namespace Playground.Infrastructure.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly PlaygroundDbContext _context;

    public RecipeRepository(PlaygroundDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(Recipe recipe)
    {
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task<Recipe?> GetByIdAsync(Guid id)
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
                .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Restaurant)
            .FirstOrDefaultAsync(r => r.RecipeId == id);
    }

    public async Task UpdateAsync(Recipe recipe)
    {
        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
                .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Restaurant)
            .ToListAsync();
    }
}