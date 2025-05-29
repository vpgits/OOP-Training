namespace OOPTraining.Common.Abstractions.Data;

public record PizzaOrder
{
    public string Name { get; init; } = "Custom Pizza";
    public OrderPizzaSize Size { get; init; }
    public List<OrderPizzaTopping> Toppings { get; init; } = new List<OrderPizzaTopping>();

    public PizzaOrder(string name, OrderPizzaSize size, List<OrderPizzaTopping> toppings)
    {
        Name = name ?? $"Pizza {string.Join(", ", toppings.Select(t => t.ToString()))} {size}";
        Size = size;
        Toppings = toppings ?? new List<OrderPizzaTopping>();
    }
}