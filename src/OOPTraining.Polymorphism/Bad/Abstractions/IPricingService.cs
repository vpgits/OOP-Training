using OOPTraining.Polymorphism.Bad.Entities;

namespace OOPTraining.Polymorphism.Bad.Abstractions;

public interface IPricingService
{
    decimal CalculateOrderTotal(Order order);
}