using Microsoft.AspNetCore.Mvc;
using Playground.Application.DTOs;
using Playground.Application.Interfaces;

namespace Playground.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly ICreateRecipeUseCase _createRecipeUseCase;
    private readonly IUpdateRecipeUseCase _updateRecipeUseCase;
    private readonly IGetRecipeUseCase _getRecipeUseCase;
    private readonly ICalculateRecipeCostUseCase _calculateRecipeCostUseCase;

    public RecipesController(
        ICreateRecipeUseCase createRecipeUseCase,
        IUpdateRecipeUseCase updateRecipeUseCase,
        IGetRecipeUseCase getRecipeUseCase,
        ICalculateRecipeCostUseCase calculateRecipeCostUseCase)
    {
        _createRecipeUseCase = createRecipeUseCase;
        _updateRecipeUseCase = updateRecipeUseCase;
        _getRecipeUseCase = getRecipeUseCase;
        _calculateRecipeCostUseCase = calculateRecipeCostUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var recipeDto = await _createRecipeUseCase.ExecuteAsync(dto.Name, dto.ProfitMargin, dto.Ingredients, dto.RestaurantId);
            return CreatedAtAction(nameof(GetRecipe), new { recipeId = recipeDto.RecipeId }, recipeDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] UpdateRecipeDto dto)
    {
        if (!ModelState.IsValid || id != dto.Id)
            return BadRequest(ModelState);

        try
        {
            var recipeDto = await _updateRecipeUseCase.ExecuteAsync(id, dto.Name, dto.ProfitMargin, dto.Ingredients);
            return Ok(recipeDto);
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
    public async Task<IActionResult> GetRecipe(Guid id)
    {
        try
        {
            var recipeDto = await _getRecipeUseCase.GetById(id);
            if (recipeDto == null)
                return NotFound(new { Error = "Receita não encontrada." });
            return Ok(recipeDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRecipes()
    {
        try
        {
            var recipes = await _getRecipeUseCase.GetAllAsync();
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("{id}/cost")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CalculateRecipeCost(Guid id)
    {
        try
        {
            var cost = await _calculateRecipeCostUseCase.ExecuteAsync(id);
            return Ok(new { Amount = cost.Amount, Currency = cost.Currency });
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
}