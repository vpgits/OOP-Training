using OOPTraining.Polymorphism.Good.Abstractions;

namespace OOPTraining.Polymorphism.Good.Entities;

public abstract class MenuItem : BaseEntity, IOrderable
{
    public string Name { get; init; } = string.Empty;
    public decimal BasePrice { get; init; }

    public abstract decimal CalculatePrice();

    public virtual string GetDisplayName() => Name;
    public virtual decimal GetPrice() => CalculatePrice();
    public virtual bool IsAvailable() => true;
}