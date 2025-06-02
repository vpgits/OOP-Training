using OOPTraining.AbstractionAndInheritance.Bad.Abstractions;
using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Bad.Entities;

public class Beverage : IMenuItem
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    private BeverageSize _size;
    private BeverageTemperature _temperature;

    public Beverage(string name, BeverageSize size, BeverageTemperature temperature = BeverageTemperature.Cold)
    {
        Id = Guid.NewGuid();
        Name = name;
        _size = size;
        _temperature = temperature;
        Price = MenuManager.CalculateBeveragePrice(size, temperature);
    }


    public bool SetTemperature(BeverageTemperature temperature)
    {
        _temperature = temperature;
        return true;
    }

    public BeverageTemperature GetTemperature()
    {
        return _temperature;
    }

    public bool SetSize(BeverageSize size)
    {
        _size = size;
        return true;
    }

    public BeverageSize GetBeverageSize()
    {
        return _size;
    }


    public bool AddTopping(PizzaTopping topping)
    {

        throw new NotImplementedException("Beverages do not support toppings.");
    }

    public bool RemoveTopping(PizzaTopping topping)
    {

        throw new NotImplementedException("Beverages do not support toppings.");
    }

    public List<PizzaTopping> GetToppings()
    {

        throw new NotImplementedException("Beverages do not support toppings.");
    }

    public PizzaSize GetSize()
    {

        throw new NotImplementedException("Beverages do not have a pizza size.");
    }

    public decimal CalculatePrice()
    {
        return Price;
    }

    public string GetDescription()
    {
        return $"{Name} ({_size}, {_temperature}) - ${Price:F2}";
    }
}