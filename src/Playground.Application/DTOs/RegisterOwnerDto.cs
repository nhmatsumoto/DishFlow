namespace Playground.Application.DTOs;

public class RegisterOwnerDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
}
