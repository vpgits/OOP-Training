using System.Drawing;

namespace OOPTraining.Common.Abstractions.Data;

public record PizzaOrder
{
    public string Name { get; init; } = string.Empty;
    public OrderPizzaSize Size { get; init; }
    public List<OrderPizzaTopping> Toppings { get; init; } = new List<OrderPizzaTopping>();

    public PizzaOrder()
    {
        Name = $"Pizza {string.Join(", ", Toppings.Select(t => t.ToString()))} {Size}";
    }
}
