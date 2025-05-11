using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using System.Security.Claims;

namespace Playground.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly ICreateRestaurantUseCase _createRestaurantUseCase;
    private readonly IUpdateRestaurantUseCase _updateRestaurantUseCase;
    private readonly IGetRestaurantUseCase _getRestaurantUseCase;
    private readonly IRegisterRestaurantOwnerUseCase _registerOwnerUseCase;

    public RestaurantsController(
        ICreateRestaurantUseCase createRestaurantUseCase,
        IUpdateRestaurantUseCase updateRestaurantUseCase,
        IGetRestaurantUseCase getRestaurantUseCase,
        IRegisterRestaurantOwnerUseCase registerOwnerUseCase)
    {
        _createRestaurantUseCase = createRestaurantUseCase;
        _updateRestaurantUseCase = updateRestaurantUseCase;
        _getRestaurantUseCase = getRestaurantUseCase;
        _registerOwnerUseCase = registerOwnerUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            var username = User.Identity?.Name ?? "unknown";
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";

            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized(new { Error = "Usuário não autenticado corretamente." });

            // 1. Cria o restaurante
            var restaurantDto = await _createRestaurantUseCase.ExecuteAsync(dto.Name);

            // 2. Registra o usuário como proprietário do restaurante
            await _registerOwnerUseCase.ExecuteAsync(new RegisterOwnerDto
            {
                UserId = userId,
                Username = username,
                Email = email,
                RestaurantId = restaurantDto.RestaurantId
            });


            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurantDto.RestaurantId }, restaurantDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{restaurantId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant(Guid restaurantId, [FromBody] UpdateRestaurantDto dto)
    {
        if (!ModelState.IsValid || restaurantId != dto.RestaurantId)
            return BadRequest(ModelState);

        try
        {
            var restaurantDto = await _updateRestaurantUseCase.ExecuteAsync(restaurantId, dto.Name, dto.TotalDishesSold);
            return Ok(restaurantDto);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRestaurant(Guid id)
    {
        try
        {
            var restaurantDto = await _getRestaurantUseCase.GetByIdAsync(id);
            if (restaurantDto == null)
                return NotFound(new { Error = "Restaurante não encontrado." });
            return Ok(restaurantDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRestaurants()
    {
        try
        {
            var restaurants = await _getRestaurantUseCase.GetAllAsync();
            return Ok(restaurants);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}