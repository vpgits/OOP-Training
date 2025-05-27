using OOPTraining.AbstractionAndInheritance.Good.Abstractions;
using OOPTraining.AbstractionAndInheritance.Good.Entities;

namespace OOPTraining.AbstractionAndInheritance.Good.Services;

public class PricingService : IPricingService
{
    public decimal CalculateItemPrice(MenuItem item)
    {
        return item.CalculatePrice();
    }

    public decimal CalculateOrderTotal(Order order)
    {
        return order.CalculateTotal();
    }

    public decimal ApplyDiscount(decimal amount, decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage));

        return amount * (1 - discountPercentage / 100);
    }
}