using OOPTraining.Polymorphism.Good.Abstractions;

namespace OOPTraining.Polymorphism.Good.Entities;

public class OrderableCollection : List<IOrderable>
{
    public decimal TotalPrice => this.Sum(item => item.GetPrice());

    public IEnumerable<IOrderable> AvailableItems => this.Where(item => item.IsAvailable());

    public IEnumerable<string> DisplayNames => this.Select(item => item.GetDisplayName());

    public void AddIfAvailable(IOrderable item)
    {
        if (item.IsAvailable())
        {
            Add(item);
        }
    }

    public OrderableCollection GetAvailableItems()
    {
        var available = new OrderableCollection();
        available.AddRange(AvailableItems);
        return available;
    }

    public void PrintOrder()
    {
        Console.WriteLine("Order Summary:");
        foreach (var item in this)
        {
            Console.WriteLine($"- {item.GetDisplayName()}: ${item.GetPrice():F2}");
        }
        Console.WriteLine($"Total: ${TotalPrice:F2}");
    }
}