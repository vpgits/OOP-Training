using OOPTraining.AbstractionAndInheritance.Good.Entities;

namespace OOPTraining.AbstractionAndInheritance.Good.Abstractions;

public interface IPricingService
{
    decimal CalculateItemPrice(MenuItem item);
    decimal CalculateOrderTotal(Order order);
    decimal ApplyDiscount(decimal amount, decimal discountPercentage);
}