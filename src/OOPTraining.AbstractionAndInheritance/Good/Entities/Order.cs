namespace OOPTraining.AbstractionAndInheritance.Good.Entities;

public class Order : BaseEntity
{
    public Customer Customer { get; set; } = default!;
    public List<MenuItem> Items { get; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public void AddItem(MenuItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        Items.Add(item);
    }

    public decimal CalculateTotal()
    {
        return Items.Sum(item => item.CalculatePrice());
    }

    public bool CanComplete()
    {
        return Customer.IsValid() && Items.Any() && Status == OrderStatus.Draft;
    }

    public void Complete()
    {
        if (!CanComplete()) throw new InvalidOperationException("Order cannot be completed");
        Status = OrderStatus.Completed;
    }
}