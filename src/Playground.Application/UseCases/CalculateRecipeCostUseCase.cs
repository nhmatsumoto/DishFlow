using Playground.Application.Interfaces;
using Playground.Domain.Repository;
using Playground.Domain.ValueObjects;

namespace Playground.Application.UseCases;

public class CalculateRecipeCostUseCase : ICalculateRecipeCostUseCase
{
    private readonly IRecipeRepository _recipeRepository;

    public CalculateRecipeCostUseCase(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
    }

    public async Task<Money> ExecuteAsync(Guid recipeId)
    {
        var recipe = await _recipeRepository.GetByIdAsync(recipeId);
        if (recipe == null)
            throw new InvalidOperationException($"Receita com ID {recipeId} não encontrada.");

        return recipe.CalculateDirectCost();
    }
}
