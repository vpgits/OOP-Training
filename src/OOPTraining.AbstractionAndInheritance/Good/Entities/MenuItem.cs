namespace OOPTraining.AbstractionAndInheritance.Good.Entities;

public abstract class MenuItem : BaseEntity
{
    public virtual string Name { get; init; } = string.Empty;
    public virtual decimal BasePrice { get; init; }

    public abstract decimal CalculatePrice();
}