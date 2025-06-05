namespace OOPTraining.AbstractionAndInheritance.Good.Entities;

public abstract class MenuItem : BaseEntity
{
    public string Name { get; init; } = string.Empty;
    public decimal BasePrice { get; set; }

    public abstract decimal CalculatePrice();

    public MenuItem(string name, decimal basePrice)
    {
        Name = name;
        BasePrice = basePrice;
    }
}