using Microsoft.EntityFrameworkCore;
using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Infrastructure.Context;

namespace Playground.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PlaygroundDbContext _context;

    public UserRepository(PlaygroundDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.Restaurant)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
