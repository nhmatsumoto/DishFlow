namespace Playground.Domain.ValueObjects;

/// <summary>
/// Representa uma categoria de custo.
/// </summary>
public class CostCategory : ValueObject
{
    public string Name { get; }

    /// <summary>
    /// Cria uma nova categoria de custo.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public CostCategory(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("A categoria não pode ser vazia.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}