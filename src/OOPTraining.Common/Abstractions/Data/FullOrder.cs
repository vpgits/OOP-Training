namespace OOPTraining.Common.Abstractions.Data;

public record FullOrder()
{
    public OrderCustomer Customer { get; init; } = default!;
    public PizzaOrder? PizzaOrder { get; init; }
    public BeverageOrder? BeverageOrder { get; init; }
}