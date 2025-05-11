using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Repository;

namespace Playground.Application.UseCases;

public class GetIngredientUseCase : IGetIngredientUseCase
{
    private readonly IIngredientRepository _ingredientRepository;

    public GetIngredientUseCase(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
    }

    public async Task<IngredientDto?> GetByIdAsync(Guid id)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(id);
        if (ingredient == null)
            return null;

        return new IngredientDto
        {
            IngredientId = ingredient.IngredientId,
            Name = ingredient.Name,
            UnitPriceAmount = ingredient.UnitPrice.Amount,
            UnitPriceCurrency = ingredient.UnitPrice.Currency,
            UnitSymbol = ingredient.Unit.Symbol,
            CreatedAt = ingredient.CreatedAt,
            UpdatedAt = ingredient.UpdatedAt
        };
    }

    public async Task<IEnumerable<IngredientDto>> GetAllAsync()
    {
        var ingredients = await _ingredientRepository.GetAllAsync();
        return ingredients.Select(i => new IngredientDto
        {
            IngredientId = i.IngredientId,
            Name = i.Name,
            UnitPriceAmount = i.UnitPrice.Amount,
            UnitPriceCurrency = i.UnitPrice.Currency,
            UnitSymbol = i.Unit.Symbol,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt
        }).ToList();
    }
}
