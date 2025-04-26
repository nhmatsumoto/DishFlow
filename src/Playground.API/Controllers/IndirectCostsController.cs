using Microsoft.AspNetCore.Mvc;
using Playground.Application.DTOs;
using Playground.Application.Interfaces;

namespace Playground.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndirectCostsController : ControllerBase
{
    private readonly ICreateIndirectCostUseCase _createIndirectCostUseCase;
    private readonly IUpdateIndirectCostUseCase _updateIndirectCostUseCase;
    private readonly IGetIndirectCostUseCase _getIndirectCostUseCase;

    public IndirectCostsController(
        ICreateIndirectCostUseCase createIndirectCostUseCase,
        IUpdateIndirectCostUseCase updateIndirectCostUseCase,
        IGetIndirectCostUseCase getIndirectCostUseCase)
    {
        _createIndirectCostUseCase = createIndirectCostUseCase;
        _updateIndirectCostUseCase = updateIndirectCostUseCase;
        _getIndirectCostUseCase = getIndirectCostUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateIndirectCost([FromBody] CreateIndirectCostDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _createIndirectCostUseCase.ExecuteAsync(dto.Name, dto.Amount, dto.Currency, dto.PeriodType, dto.CategoryName, dto.RestaurantId);
            return CreatedAtAction(nameof(GetIndirectCost), new { id = Guid.NewGuid() }, null); // ID gerado pelo repositório
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateIndirectCost(Guid id, [FromBody] UpdateIndirectCostDto dto)
    {
        if (!ModelState.IsValid || id != dto.IndirectCostId)
            return BadRequest(ModelState);

        try
        {
            await _updateIndirectCostUseCase.ExecuteAsync(id, dto.Name, dto.Amount, dto.Currency, dto.PeriodType, dto.CategoryName);
            return NoContent();
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
    public async Task<IActionResult> GetIndirectCost(Guid id)
    {
        try
        {
            var indirectCostDto = await _getIndirectCostUseCase.ExecuteAsync(id);
            if (indirectCostDto == null)
                return NotFound(new { Error = "Custo indireto não encontrado." });
            return Ok(indirectCostDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}