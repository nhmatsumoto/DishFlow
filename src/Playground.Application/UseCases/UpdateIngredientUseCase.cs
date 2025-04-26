using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Repository;
using Playground.Domain.ValueObjects;

namespace Playground.Application.UseCases;

public class UpdateIngredientUseCase : IUpdateIngredientUseCase
{
    private readonly IIngredientRepository _ingredientRepository;

    public UpdateIngredientUseCase(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
    }

    public async Task<IngredientDto> ExecuteAsync(Guid id, string name, decimal unitPriceAmount, string unitPriceCurrency, string unitSymbol)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(id);
        if (ingredient == null)
            throw new InvalidOperationException($"Ingrediente com ID {id} não encontrado.");

        ingredient.UpdateName(name);
        ingredient.UpdatePrice(new Money(unitPriceAmount, unitPriceCurrency));
        ingredient.UpdateUnit(UnitOfMeasure.FromSymbol(unitSymbol));

        await _ingredientRepository.UpdateAsync(ingredient);

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
}
