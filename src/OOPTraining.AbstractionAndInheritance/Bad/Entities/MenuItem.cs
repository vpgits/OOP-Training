
using OOPTraining.AbstractionAndInheritance.Bad.Abstractions;
using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Bad.Entities;

public abstract class MenuItem : IMenuItem
{
    public Guid Id { get; init; }
    public virtual string Name { get; set; }
    public  decimal Price { get; set; }
    public MenuItem(string name, decimal price)
    {
        Name = name;
        Price = price;
        Id = Guid.NewGuid();
    }

    public bool AddTopping(PizzaTopping topping)
    {
        throw new NotImplementedException();
    }

    public bool RemoveTopping(PizzaTopping topping)
    {
        throw new NotImplementedException();
    }

    public List<PizzaTopping> GetToppings()
    {
        throw new NotImplementedException();
    }

    public PizzaSize GetSize()
    {
        throw new NotImplementedException();
    }

    public bool SetTemperature(BeverageTemperature temperature)
    {
        throw new NotImplementedException();
    }

    public BeverageTemperature GetTemperature()
    {
        throw new NotImplementedException();
    }

    public bool SetSize(BeverageSize size)
    {
        throw new NotImplementedException();
    }

    public BeverageSize GetBeverageSize()
    {
        throw new NotImplementedException();
    }

    public virtual decimal CalculatePrice()
    {
        throw new NotImplementedException();
    }

    public virtual string GetDescription()
    {
        throw new NotImplementedException();
    }
}