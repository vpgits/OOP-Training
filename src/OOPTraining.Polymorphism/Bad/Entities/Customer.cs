namespace OOPTraining.Polymorphism.Bad.Entities;

public partial class Customer : BaseEntity
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}