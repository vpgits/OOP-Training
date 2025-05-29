namespace OOPTraining.Polymorphism.Good.Abstractions;

public interface IOrderable
{
    string GetDisplayName();
    decimal GetPrice();
    bool IsAvailable();
}