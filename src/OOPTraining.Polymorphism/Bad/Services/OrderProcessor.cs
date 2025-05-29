using OOPTraining.Common.Entities;
using OOPTraining.Polymorphism.Bad.Entities;

public class OrderProcessor
{
    public void ProcessOrder(List<object> items)
    {
        foreach (var item in items)
        {
            var typeName = item.GetType().Name;

            if (typeName.Contains("Pizza"))
            {
                ProcessPizzaItem((Pizza)item);
            }
            else if (typeName.Contains("Beverage"))
            {
                ProcessBeverageItem((Beverage)item);
            }
        }
    }

    private void ProcessPizzaItem(Pizza pizza)
    {
        Console.WriteLine($"Processing pizza: {pizza.Name}");
    }

    private void ProcessBeverageItem(Beverage beverage)
    {
        Console.WriteLine($"Processing beverage: {beverage.Name}");
    }

    public decimal CalculatePrice(object item)
    {
        if (item is Pizza)
        {
            return CalculatePizzaPrice((Pizza)item);
        }

        if (item is Beverage)
        {
            return CalculateBeveragePrice((Beverage)item);
        }

        throw new ArgumentException("Unknown item type");
    }

    private decimal CalculatePizzaPrice(Pizza pizza)
    {
        return pizza.BasePrice + (pizza.Toppings.Count * 2.50m);
    }

    private decimal CalculateBeveragePrice(Beverage beverage)
    {
        return beverage.Size switch
        {
            BeverageSize.Small => 2.99m,
            BeverageSize.Medium => 3.99m,
            BeverageSize.Large => 4.99m,
            _ => 0m
        };
    }
}