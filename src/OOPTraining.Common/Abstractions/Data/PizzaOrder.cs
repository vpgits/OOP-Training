namespace OOPTraining.Common.Abstractions.Data;

public record PizzaOrder
{
    public OrderPizzaSize Size { get; init; }
    public List<OrderPizzaTopping> Toppings { get; init; } = new List<OrderPizzaTopping>();
}
