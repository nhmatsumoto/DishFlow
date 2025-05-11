using Playground.Domain.Entities;

namespace Playground.Domain.Repository;

public interface IRestaurantRepository
{
    Task AddAsync(Restaurant restaurant);
    Task<Restaurant?> GetByIdAsync(Guid id);
    Task UpdateAsync(Restaurant restaurant);
    Task<IEnumerable<Restaurant>> GetAllAsync();
}
