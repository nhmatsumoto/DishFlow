using Playground.Application.DTOs;
using Playground.Application.Interfaces;
using Playground.Domain.Entities;
using Playground.Domain.Repository;
using Playground.Domain.ValueObjects;

namespace Playground.Application.UseCases;

public class CreateIngredientUseCase : ICreateIngredientUseCase
{
    private readonly IIngredientRepository _ingredientRepository;

    public CreateIngredientUseCase(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
    }

    public async Task<IngredientDto> ExecuteAsync(string name, decimal unitPriceAmount, string unitPriceCurrency, string unitSymbol)
    {
        var ingredient = new Ingredient(
            name,
            new Money(unitPriceAmount, unitPriceCurrency),
            UnitOfMeasure.FromSymbol(unitSymbol));

        await _ingredientRepository.AddAsync(ingredient);

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
