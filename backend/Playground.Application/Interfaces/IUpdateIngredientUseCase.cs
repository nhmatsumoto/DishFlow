using Playground.Application.DTOs;

namespace Playground.Application.Interfaces;

public interface IUpdateIngredientUseCase
{
    Task<IngredientDto> ExecuteAsync(Guid id, string name, decimal unitPriceAmount, string unitPriceCurrency, string unitSymbol);
}
