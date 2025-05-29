using OOPTraining.Polymorphism.Bad.Entities;

namespace OOPTraining.Polymorphism.Bad.Services;

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
}