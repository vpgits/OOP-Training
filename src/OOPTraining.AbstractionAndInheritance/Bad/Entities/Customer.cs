using OOPTraining.AbstractionAndInheritance.Bad.Abstractions;
using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Bad.Entities;

public class Customer
{
    public Guid Id { get; private init; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Customer(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }


    public void CustomizePizza(Pizza pizza, List<PizzaTopping> toppings)
    {
        toppings.ForEach(topping =>
        {
            if (!pizza.GetToppings().Contains(topping))
            {
                pizza.AddTopping(topping);
            }
        });

        if (pizza.GetSize() == PizzaSize.Large && pizza.GetToppings().Count < 3)
        {
            pizza.AddTopping(PizzaTopping.ExtraCheese);
        }
    }

    public void CustomizeBeverage(Beverage beverage, BeverageTemperature temperature)
    {

        beverage.SetTemperature(temperature);


        if (beverage.GetBeverageSize() == BeverageSize.Large)
        {

        }
    }

    public decimal GetTotalCost(List<IMenuItem> items)
    {
        decimal total = 0;
        foreach (var item in items)
        {

            if (item is Pizza pizza)
            {
                total += MenuManager.CalculatePizzaPrice(pizza.GetSize(), pizza.GetToppings());
            }
            else if (item is Beverage beverage)
            {
                total += MenuManager.CalculateBeveragePrice(beverage.GetBeverageSize(), beverage.GetTemperature());
            }
        }
        return total;
    }
}