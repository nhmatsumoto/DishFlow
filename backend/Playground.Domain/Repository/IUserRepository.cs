using Playground.Domain.Entities;

namespace Playground.Domain.Repository;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid userId);
    Task UpdateAsync(User user);
}