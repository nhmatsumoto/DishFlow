namespace Playground.Application.Interfaces;

public interface ICreateIndirectCostUseCase
{
    Task ExecuteAsync(string name, decimal amount, string currency, string periodType, string categoryName, Guid restaurantId);
}
