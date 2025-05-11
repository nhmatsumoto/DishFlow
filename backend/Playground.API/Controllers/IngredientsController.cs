using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.DTOs;
using Playground.Application.Interfaces;

namespace Playground.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly ICreateIngredientUseCase _createIngredientUseCase;
    private readonly IUpdateIngredientUseCase _updateIngredientUseCase;
    private readonly IGetIngredientUseCase _getIngredientUseCase;

    public IngredientsController(
        ICreateIngredientUseCase createIngredientUseCase,
        IUpdateIngredientUseCase updateIngredientUseCase,
        IGetIngredientUseCase getIngredientUseCase)
    {
        _createIngredientUseCase = createIngredientUseCase;
        _updateIngredientUseCase = updateIngredientUseCase;
        _getIngredientUseCase = getIngredientUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var ingredientDto = await _createIngredientUseCase.ExecuteAsync(dto.Name, dto.UnitPriceAmount, dto.UnitPriceCurrency, dto.UnitSymbol);
            return CreatedAtAction(nameof(GetIngredient), new { id = ingredientDto.IngredientId }, ingredientDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllIngredients()
    {
        try
        {
            var ingredients = await _getIngredientUseCase.GetAllAsync();
            return Ok(ingredients);
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
    public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] UpdateIngredientDto dto)
    {
        if (!ModelState.IsValid || id != dto.Id)
            return BadRequest(ModelState);

        try
        {
            var ingredientDto = await _updateIngredientUseCase.ExecuteAsync(id, dto.Name, dto.UnitPriceAmount, dto.UnitPriceCurrency, dto.UnitSymbol);
            return Ok(ingredientDto);
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
    public async Task<IActionResult> GetIngredient(Guid id)
    {
        try
        {
            var ingredientDto = await _getIngredientUseCase.GetByIdAsync(id);
            if (ingredientDto == null)
                return NotFound(new { Error = "Ingrediente não encontrado." });
            return Ok(ingredientDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}