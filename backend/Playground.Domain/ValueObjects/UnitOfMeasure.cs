namespace Playground.Domain.ValueObjects;

/// <summary>
/// Representa uma unidade de medida para ingredientes (ex.: kg, g, L, ml, unit).
/// </summary>
public class UnitOfMeasure : ValueObject
{
    public string Symbol { get; }

    private UnitOfMeasure(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("O símbolo da unidade não pode ser vazio.");
        Symbol = symbol;
    }

    public static readonly UnitOfMeasure Kg = new("kg");
    public static readonly UnitOfMeasure Gram = new("g");
    public static readonly UnitOfMeasure Liter = new("L");
    public static readonly UnitOfMeasure Ml = new("ml");
    public static readonly UnitOfMeasure Unit = new("unit");

    public static UnitOfMeasure FromSymbol(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("O símbolo não pode ser vazio.");

        return symbol.ToLower() switch
        {
            "kg" => Kg,
            "g" => Gram,
            "l" => Liter,
            "ml" => Ml,
            "unit" => Unit,
            _ => throw new ArgumentException($"Unidade inválida: {symbol}")
        };
    }

    public decimal GetConversionFactor(UnitOfMeasure targetUnit)
    {
        return (Symbol, targetUnit.Symbol) switch
        {
            ("kg", "g") => 1000m,
            ("g", "kg") => 0.001m,
            ("L", "ml") => 1000m,
            ("ml", "L") => 0.001m,
            _ when Symbol == targetUnit.Symbol => 1m,
            _ => throw new InvalidOperationException($"Conversão não suportada: {Symbol} para {targetUnit.Symbol}")
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Symbol;
    }

    public override string ToString() => Symbol;
}
