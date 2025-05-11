namespace Playground.Application.Interfaces
{
    public interface IUpdateIndirectCostUseCase
    {
        Task ExecuteAsync(Guid id, string name, decimal amount, string currency, string periodType, string categoryName);
    }
}
