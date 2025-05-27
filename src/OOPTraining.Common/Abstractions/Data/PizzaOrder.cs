namespace OOPTraining.Common.Abstractions.Data;

public record PizzaOrder
{
    public string CustomerName { get; init; } = string.Empty;
    public OrderPizzaSize Size { get; init; }
    public List<OrderPizzaTopping> Toppings { get; init; } = new List<OrderPizzaTopping>();
}
