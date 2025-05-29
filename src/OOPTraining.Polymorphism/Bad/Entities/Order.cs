namespace OOPTraining.Polymorphism.Bad.Entities;

public class Order : BaseEntity
{
    public Customer Customer { get; set; } = default!;
    public List<MenuItem> MenuItems { get; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public void AddItem(MenuItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        MenuItems.Add(item);
    }

    public decimal CalculateTotal()
    {
        return MenuItems.Sum(item => item.CalculatePrice());
    }

    public bool CanComplete()
    {
        return Customer.IsValid() && MenuItems.Any() && Status == OrderStatus.Draft;
    }

    public void Complete()
    {
        if (!CanComplete()) throw new InvalidOperationException("Order cannot be completed");
        Status = OrderStatus.Completed;
    }
}