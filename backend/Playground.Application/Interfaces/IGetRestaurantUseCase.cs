using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface IGetRestaurantUseCase
{
    Task<RestaurantDto> GetByIdAsync(Guid id);
    Task<IEnumerable<RestaurantDto>> GetAllAsync();
}
