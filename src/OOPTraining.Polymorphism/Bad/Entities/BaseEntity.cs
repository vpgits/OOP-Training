using OOPTraining.Polymorphism.Bad.Abstractions;

namespace OOPTraining.Polymorphism.Bad.Entities;

public abstract class BaseEntity : IAuditable
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; internal set; } = DateTime.UtcNow;
    public string CreatedBy { get; init; } = string.Empty;
    public string UpdatedBy { get; internal set; } = string.Empty;
}