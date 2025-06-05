using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Good.Entities;

public sealed class Pizza : MenuItem
{
    public PizzaSize Size { get; init; }
    public List<PizzaTopping> Toppings { get; init; } = new();

    public sealed override decimal CalculatePrice()
    {
        var sizeMultiplier = Size switch
        {
            PizzaSize.Small => 1.0m,
            PizzaSize.Medium => 1.5m,
            PizzaSize.Large => 2.0m,
            _ => 1.0m
        };

        var toppingCost = Toppings.Count * 1.50m;
        return BasePrice * sizeMultiplier + toppingCost;
    }

    public Pizza(string name, decimal basePrice, PizzaSize size, List<PizzaTopping> toppings)
    : base(name, basePrice)
    {
        Size = size;
        Toppings = toppings;
    }
}