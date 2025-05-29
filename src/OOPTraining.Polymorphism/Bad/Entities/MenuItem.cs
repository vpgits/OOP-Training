namespace OOPTraining.Polymorphism.Bad.Entities;

public abstract class MenuItem : BaseEntity
{
    public string Name { get; init; } = string.Empty;
    public decimal BasePrice { get; init; }

    public abstract decimal CalculatePrice();
}