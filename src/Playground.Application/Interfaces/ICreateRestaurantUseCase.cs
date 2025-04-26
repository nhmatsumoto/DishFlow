using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface ICreateRestaurantUseCase
{
    Task<RestaurantDto> ExecuteAsync(string name);
}
