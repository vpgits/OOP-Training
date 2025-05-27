using OOPTraining.AbstractionAndInheritance.Bad.Abstractions;
using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Bad.Entities;

public static class MenuManager
{
    
    private static readonly Dictionary<PizzaSize, decimal> PizzaBasePrices = new()
    {
        { PizzaSize.Small, 8.99m },
        { PizzaSize.Medium, 11.99m },
        { PizzaSize.Large, 14.99m },
        { PizzaSize.ExtraLarge, 17.99m }
    };

    private static readonly Dictionary<BeverageSize, decimal> BeverageBasePrices = new()
    {
        { BeverageSize.Small, 1.99m },
        { BeverageSize.Medium, 2.49m },
        { BeverageSize.Large, 2.99m }
    };

    
    public static decimal CalculatePizzaPrice(PizzaSize size, List<PizzaTopping> toppings)
    {
        var basePrice = PizzaBasePrices[size];
        var toppingsPrice = toppings.Count * 1.50m;
        return basePrice + toppingsPrice;
    }

    public static decimal CalculateBeveragePrice(BeverageSize size, BeverageTemperature temperature)
    {
        var basePrice = BeverageBasePrices[size];
        
        if (temperature == BeverageTemperature.Hot) basePrice += 0.50m;
        return basePrice;
    }

    public static bool ValidateMenuItem(IMenuItem item)
    {
        
        if (item is Pizza pizza)
        {
            return pizza.GetToppings().Count <= 10;
        }
        else if (item is Beverage beverage)
        {
            return !string.IsNullOrEmpty(beverage.Name);
        }
        return false;
    }

    public static string GetItemDescription(IMenuItem item)
    {
        
        if (item is Pizza pizza)
        {
            return $"Pizza: {pizza.Name} with {pizza.GetToppings().Count} toppings";
        }
        else if (item is Beverage beverage)
        {
            return $"Beverage: {beverage.Name} ({beverage.GetBeverageSize()})";
        }
        return "Unknown item";
    }

    public static void ProcessOrder(Customer customer, List<IMenuItem> items)
    {
        
        foreach (var item in items)
        {
            if (!ValidateMenuItem(item))
            {
                Console.WriteLine($"Invalid item: {item.Name}");
                continue;
            }

            Console.WriteLine(GetItemDescription(item));
        }

        var total = customer.GetTotalCost(items);
        Console.WriteLine($"Total for {customer.Name}: ${total:F2}");
    }
}