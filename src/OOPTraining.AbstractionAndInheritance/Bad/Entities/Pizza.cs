using OOPTraining.AbstractionAndInheritance.Bad.Abstractions;
using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Bad.Entities;

public class Pizza(PizzaSize size, string name) : MenuItem(name, MenuManager.CalculatePizzaPrice(size, new List<PizzaTopping>())), IMenuItem
{
    public new Guid Id { get; private init; }
    public new string Name { get; private set; } = string.Empty;
    public new decimal Price { get; private set; }

    private readonly List<PizzaTopping> _toppings = new();
    private PizzaSize size = size;

    public PizzaSize Size
    {
        get => size;
        set
        {
            if (size != value)
            {
                size = value;
                Price = MenuManager.CalculatePizzaPrice(size, _toppings);
            }
        }
    }

    public new bool AddTopping(PizzaTopping topping)
    {
        if (_toppings.Contains(topping)) return false;
        if (_toppings.Count >= 10) return false;
        _toppings.Add(topping);
        return true;
    }

    public new bool RemoveTopping(PizzaTopping topping)
    {
        return _toppings.Remove(topping);
    }

    public new List<PizzaTopping> GetToppings()
    {
        return [.. _toppings];
    }

    public new PizzaSize GetSize()
    {
        return size;
    }

    public new bool SetTemperature(BeverageTemperature temperature)
    {
        throw new NotSupportedException("Pizza does not have a beverage temperature.");
    }

    public new BeverageTemperature GetTemperature()
    {
        throw new NotSupportedException("Pizza does not have a beverage temperature.");
    }

    public new bool SetSize(BeverageSize size)
    {
        throw new NotSupportedException("Pizza does not have a beverage size.");
    }

    public new BeverageSize GetBeverageSize()
    {
        throw new NotSupportedException("Pizza does not have a beverage size.");
    }

    public new decimal CalculatePrice()
    {
        return Price;
    }

    public override string GetDescription()
    {
        return $"{Name} ({size}) with {_toppings.Count} toppings - ${Price:F2}";
    }
}