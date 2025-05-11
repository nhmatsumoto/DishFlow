using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Entities;
using Playground.Domain.Repository;

namespace Playground.Application.UseCases;

public class RegisterRestaurantOwnerUseCase : IRegisterRestaurantOwnerUseCase
{
    private readonly IUserRepository _userRepository;

    public RegisterRestaurantOwnerUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task ExecuteAsync(RegisterOwnerDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);

        if (user == null)
        {
            user = new User(dto.UserId, dto.Username, dto.Email, dto.RestaurantId);
            await _userRepository.AddAsync(user);
        }
        else
        {
            user.AssignRestaurant(dto.RestaurantId);
            await _userRepository.UpdateAsync(user);
        }
    }
}
