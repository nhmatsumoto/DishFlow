using Playground.Application.DTOs;

namespace Playground.Application.Interfaces
{
    public interface ICreateIngredientUseCase
    {
        Task<IngredientDto> ExecuteAsync(string name, decimal unitPriceAmount, string unitPriceCurrency, string unitSymbol);
    }
}
