using Playground.Application.DTOs;

namespace Playground.Application.Providers;

public static class UnitProvider
{
    private static readonly IReadOnlyList<UnitDto> _units = new List<UnitDto>
    {
        new() { Symbol = "kg", Description = "Quilograma" },
        new() { Symbol = "g", Description = "Grama" },
        new() { Symbol = "l", Description = "Litro" },
        new() { Symbol = "ml", Description = "Mililitro" },
        new() { Symbol = "unit", Description = "Unidade" },
    };

    public static IReadOnlyList<UnitDto> GetAll() => _units;
}
