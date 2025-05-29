using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Bad.Abstractions;

public interface IMenuItem
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public bool AddTopping(PizzaTopping topping);
    public bool RemoveTopping(PizzaTopping topping);
    public List<PizzaTopping> GetToppings();
    public PizzaSize GetSize();


    public bool SetTemperature(BeverageTemperature temperature);
    public BeverageTemperature GetTemperature();
    public bool SetSize(BeverageSize size);
    public BeverageSize GetBeverageSize();


    public decimal CalculatePrice();
    public string GetDescription();
}