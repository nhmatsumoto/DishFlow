using Playground.Application.DTOs;

namespace Playground.Application.Interfaces
{
    public interface IGetIngredientUseCase
    {
        Task<IngredientDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<IngredientDto>> GetAllAsync();
    }
}
