using OOPTraining.Common.Entities;

namespace OOPTraining.Encapsulation.Models;

internal static class PriceCalculator
{
    private const decimal ToppingPrice = 1.50m;

    private static readonly Dictionary<PizzaSize, decimal> BasePrices = new()
        {
            { PizzaSize.Small, 8.99m },
            { PizzaSize.Medium, 11.99m },
            { PizzaSize.Large, 14.99m },
            { PizzaSize.ExtraLarge, 17.99m }
        };

    internal static decimal CalculatePrice(PizzaSize size, ICollection<PizzaTopping> toppings)
    {
        var basePrice = BasePrices[size];
        var toppingsPrice = toppings.Count * ToppingPrice;
        return basePrice + toppingsPrice;
    }

    internal static decimal GetBasePrice(PizzaSize size)
    {
        return BasePrices[size];
    }
}