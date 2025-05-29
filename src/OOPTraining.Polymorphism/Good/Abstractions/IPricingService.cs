using OOPTraining.Polymorphism.Good.Entities;

namespace OOPTraining.Polymorphism.Good.Abstractions;

public interface IPricingService
{
    decimal CalculateItemPrice(MenuItem item);
    decimal CalculateOrderTotal(List<MenuItem> items);
}