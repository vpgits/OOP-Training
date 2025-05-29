using OOPTraining.Common.Entities;
using OOPTraining.Polymorphism.Bad.Abstractions;
using OOPTraining.Polymorphism.Bad.Entities;

namespace OOPTraining.Polymorphism.Bad.Services;

public class PricingService : IPricingService
{
    public decimal CalculateOrderTotal(Order order)
    {
        decimal total = 0;

        foreach (var item in order.MenuItems)
        {
            var itemType = item.GetType();

            if (itemType == typeof(Pizza))
            {
                var pizza = (Pizza)item;
                total += pizza.BasePrice + (pizza.Toppings.Count * 2.50m);
            }
            else if (itemType == typeof(Beverage))
            {
                var beverage = (Beverage)item;
                total += beverage.Size switch
                {
                    BeverageSize.Small => 2.99m,
                    BeverageSize.Medium => 3.99m,
                    BeverageSize.Large => 4.99m,
                    _ => 0m
                };
            }
        }

        return total;
    }
}
