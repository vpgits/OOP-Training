namespace OOPTraining.Polymorphism.Bad.Abstractions;

public interface IAuditable
{
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
    string CreatedBy { get; }
    string UpdatedBy { get; }
}