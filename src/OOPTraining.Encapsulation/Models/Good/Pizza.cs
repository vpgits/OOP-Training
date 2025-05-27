using OOPTraining.Common.Abstractions.Data;
using OOPTraining.Common.Entities;

namespace OOPTraining.Encapsulation.Models.Good;

public class Pizza
{
    private readonly List<PizzaTopping> _toppings = new();

    public PizzaSize Size { get; private init; }

    public IReadOnlyList<PizzaTopping> Toppings => _toppings.AsReadOnly();

    public decimal Price => PriceCalculator.CalculatePrice(Size, _toppings);

    public Pizza(PizzaSize size)
    {
        Size = size;
    }

    public bool AddTopping(PizzaTopping topping)
    {
        if (_toppings.Contains(topping))
            return false;

        if (_toppings.Count >= 10)
            return false;

        _toppings.Add(topping);
        return true;
    }

    public bool RemoveTopping(PizzaTopping topping)
    {
        return _toppings.Remove(topping);
    }

    public override string ToString()
    {
        return $"{Size} Pizza with {_toppings.Count} toppings - ${Price:F2}";
    }
}