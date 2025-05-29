using OOPTraining.Polymorphism.Good.Abstractions;

namespace OOPTraining.Polymorphism.Good.Services;

public class PolymorphicOrderService
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

    public string FormatItemDisplay(IOrderable item)
    {
        return $"{item.GetDisplayName()} - ${item.GetPrice():F2}";
    }

    public List<IOrderable> FilterAvailableItems(List<IOrderable> items)
    {
        return items.Where(item => item.IsAvailable()).ToList();
    }

}