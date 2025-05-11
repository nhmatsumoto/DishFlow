using Playground.Domain.Common;

namespace Playground.Domain.Entities;

public class User : BaseEntity
{
    public Guid UserId { get; init; } // equivalente ao "sub" do Keycloak
    public string Username { get; private set; }
    public string Email { get; private set; }

    public Guid RestaurantId { get; private set; }
    public Restaurant Restaurant { get; private set; }

    private User() { }

    public User(Guid userId, string username, string email, Guid restaurantId)
    {
        UserId = userId;
        Username = username;
        Email = email;
        RestaurantId = restaurantId;
    }

    public void AssignRestaurant(Guid restaurantId)
    {
        if (restaurantId == Guid.Empty)
            throw new ArgumentException("Restaurante inválido.");

        RestaurantId = restaurantId;
    }
}
