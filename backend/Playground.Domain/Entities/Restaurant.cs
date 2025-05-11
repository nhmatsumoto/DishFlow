using Playground.Domain.Common;

namespace Playground.Domain.Entities;

public class Restaurant : BaseEntity
{
    public Guid RestaurantId { get; init; } = Guid.NewGuid();
    public string Name { get; private set; }
    private readonly List<IndirectCost> _indirectCosts = new();
    private readonly List<Recipe> _recipes = new();
    private readonly List<User> _owners = new();

    public int TotalDishesSold { get; private set; }

    public IReadOnlyCollection<IndirectCost> IndirectCosts => _indirectCosts.AsReadOnly();
    public IReadOnlyCollection<Recipe> Recipes => _recipes.AsReadOnly();
    public IReadOnlyCollection<User> Owners => _owners.AsReadOnly();

    private Restaurant() { }

    public Restaurant(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do restaurante não pode ser vazio.");
        Name = name;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do restaurante não pode ser vazio.");
        Name = name;
    }

    public void UpdateDishesSold(int totalDishesSold)
    {
        if (totalDishesSold < 0)
            throw new ArgumentException("O total de pratos vendidos não pode ser negativo.");
        TotalDishesSold = totalDishesSold;
    }

    public void AddOwner(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _owners.Add(user);
    }
}
