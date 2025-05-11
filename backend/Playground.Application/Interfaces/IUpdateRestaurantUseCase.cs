using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface IUpdateRestaurantUseCase
{
    Task<RestaurantDto> ExecuteAsync(Guid id, string name, int totalDishesSold);
}
