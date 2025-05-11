using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface IRegisterRestaurantOwnerUseCase
{
    Task ExecuteAsync(RegisterOwnerDto dto);
}
