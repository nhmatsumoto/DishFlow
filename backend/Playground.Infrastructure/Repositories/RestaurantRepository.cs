using Microsoft.EntityFrameworkCore;
using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Infrastructure.Context;

namespace Playground.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly PlaygroundDbContext _context;

    public RestaurantRepository(PlaygroundDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id)
    {
        return await _context.Restaurants
            .Include(r => r.IndirectCosts)
            .Include(r => r.Recipes)
            .FirstOrDefaultAsync(r => r.RestaurantId == id);
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants
            .Include(r => r.Recipes)
            .Include(r => r.IndirectCosts)
            .ToListAsync();
    }
}