using OOPTraining.Polymorphism.Good.Abstractions;

namespace OOPTraining.Polymorphism.Good.Services;

public class PolymorphicOrderService : IOrderService<IOrderable>
{

    public decimal CalculateTotalPrice(List<IOrderable> items)
    {
        return items.Sum(item => item.GetPrice());
    }

    public List<string> GetAvailableItemNames(List<IOrderable> items)
    {
        return [.. items
                .Where(item => item.IsAvailable())
                .Select(item => item.GetDisplayName())];
    }

    public string GetDisplayText(IOrderable item)
    {
        return item.GetDisplayName();
    }

    public bool IsItemAvailable(IOrderable item)
    {
        return item.IsAvailable();
    }
}