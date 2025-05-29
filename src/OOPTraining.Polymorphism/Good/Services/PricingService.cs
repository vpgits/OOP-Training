using OOPTraining.Polymorphism.Good.Abstractions;
using OOPTraining.Polymorphism.Good.Entities;

namespace OOPTraining.Polymorphism.Good.Services;

public class PricingService : IPricingService
{
    public decimal CalculateItemPrice(MenuItem item)
    {
        return item.CalculatePrice();
    }

    public decimal CalculateOrderTotal(List<MenuItem> items)
    {
        return items.Sum(item => item.CalculatePrice());
    }

    public decimal ApplyDiscount(decimal amount, decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage));

        return amount * (1 - discountPercentage / 100);
    }
}