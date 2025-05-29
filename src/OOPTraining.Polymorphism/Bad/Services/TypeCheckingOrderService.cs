using OOPTraining.Polymorphism.Bad.Entities;

namespace OOPTraining.Polymorphism.Bad.Services;

public class TypeCheckingOrderService
{
    public decimal CalculateTotalPrice(List<MenuItem> items)
    {
        decimal total = 0;

        foreach (var item in items)
        {
            if (item is Pizza pizza)
            {
                total += pizza.BasePrice;
                foreach (var topping in pizza.Toppings)
                {
                    total += 2.50m;
                }
            }
            else if (item is Beverage beverage)
            {
                switch (beverage.Size)
                {
                    case Common.Entities.BeverageSize.Small:
                        total += 2.99m;
                        break;
                    case Common.Entities.BeverageSize.Medium:
                        total += 3.99m;
                        break;
                    case Common.Entities.BeverageSize.Large:
                        total += 4.99m;
                        break;
                }
            }
        }

        return total;
    }

    public string GetDisplayText(MenuItem item)
    {
        if (item.GetType() == typeof(Pizza))
        {
            var pizza = (Pizza)item;
            return $"Pizza: {pizza.Name} with {pizza.Toppings.Count} toppings";
        }

        if (item.GetType() == typeof(Beverage))
        {
            var beverage = (Beverage)item;
            return $"Beverage: {beverage.Name} ({beverage.Size})";
        }

        return "Unknown item";
    }

    public bool IsItemAvailable(MenuItem item)
    {
        var itemType = item.GetType().Name;

        switch (itemType)
        {
            case "Pizza":
                var pizza = item as Pizza;
                return pizza != null && pizza.Toppings.Count <= 10;
            case "Beverage":
                var beverage = item as Beverage;
                return beverage != null;
            default:
                return false;
        }
    }

    public List<string> GetAvailableItemNames(List<MenuItem> items)
    {
        var availableItems = new List<string>();

        foreach (var item in items)
        {
            if (IsItemAvailable(item))
            {
                availableItems.Add(GetDisplayText(item));
            }
        }

        return availableItems;
    }
}