using Playground.Domain.ValueObjects;

namespace Playground.Application.Interfaces;

public interface ICalculateRecipeCostUseCase
{
    Task<Money> ExecuteAsync(Guid recipeId);
}
