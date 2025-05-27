using OOPTraining.Encapsulation.Models;
using OOPTraining.Encapsulation.Models.Good;

namespace OOPTraining.Encapsulation.Services;

public class OrderProcessorService
{
    private readonly List<(Customer Customer, Pizza Pizza, DateTime OrderDate)> _orders = new();

    public IReadOnlyList<(Customer Customer, Pizza Pizza, DateTime OrderDate)> Orders =>
        _orders.AsReadOnly();

    public bool PlaceOrder(Customer customer, Pizza pizza)
    {
        if (customer == null || pizza == null)
            return false;

        var orderDate = DateTime.Now;
        _orders.Add((customer, pizza, orderDate));
        return true;
    }

    public decimal GetBasePriceForSize(PizzaSize size)
    {
        return PriceCalculator.GetBasePrice(size);
    }

    public Customer? GetBestCustomer()
    {
        return _orders
            .GroupBy(o => o.Customer)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault()?.Key;
    }

    public decimal TotalRevenue => _orders.Sum(o => o.Pizza.Price);
}