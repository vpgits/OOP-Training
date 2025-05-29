using OOPTraining.Common.Entities;
using OOPTraining.Polymorphism.Good.Abstractions;

namespace OOPTraining.Polymorphism.Good.Entities;

public class Pizza : MenuItem
{
    public PizzaSize Size { get; init; }
    public List<PizzaTopping> Toppings { get; init; } = new();

    public override decimal CalculatePrice()
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

    public override string GetDisplayName()
    {
        return $"{Name} Pizza ({Size})";
    }

    public override bool IsAvailable()
    {
        return Toppings.Count <= 5;
    }
}