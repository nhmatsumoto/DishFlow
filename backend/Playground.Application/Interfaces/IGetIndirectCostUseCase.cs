using Playground.Application.DTOs;

namespace Playground.Application.Interfaces
{
    public interface IGetIndirectCostUseCase
    {
        Task<IndirectCostDto?> ExecuteAsync(Guid id);
    }
}
