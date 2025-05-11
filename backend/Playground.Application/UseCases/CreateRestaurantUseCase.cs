using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Entities;
using Playground.Domain.Repository;

namespace Playground.Application.UseCases;

public class CreateRestaurantUseCase : ICreateRestaurantUseCase
{
    private readonly IRestaurantRepository _restaurantRepository;

    public CreateRestaurantUseCase(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
    }

    public async Task<RestaurantDto> ExecuteAsync(string name)
    {
        var restaurant = new Restaurant(name);
        await _restaurantRepository.AddAsync(restaurant);

        return new RestaurantDto
        {
            RestaurantId = restaurant.RestaurantId,
            Name = restaurant.Name,
            TotalDishesSold = restaurant.TotalDishesSold,
            IndirectCosts = new List<IndirectCostDto>(),
            Recipes = new List<RecipeDto>(),
            CreatedAt = restaurant.CreatedAt,
            UpdatedAt = restaurant.UpdatedAt
        };
    }
}
