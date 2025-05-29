using OOPTraining.Common.Entities;
using OOPTraining.Polymorphism.Good.Entities;

using DTOEntities = OOPTraining.Common.Abstractions.Data;

namespace OOPTraining.Polymorphism.Good.Extensions;

public static class MappingExtensions
{
    public static Customer ToEntity(this OrderCustomer customerDto)
    {
        return new Customer
        {
            Name = customerDto.Name,
            Email = customerDto.Email,
            Phone = customerDto.Phone ?? throw new ArgumentNullException(nameof(customerDto), "Phone cannot be null.")
        };
    }

    public static Pizza ToEntity(this DTOEntities.PizzaOrder pizzaDto)
    {
        return new Pizza
        {
            Name = "Custom Pizza",
            BasePrice = 12.99m,
            Size = MapPizzaSize(pizzaDto.Size.ToString()),
            Toppings = [.. pizzaDto.Toppings.Select(t => MapPizzaTopping(t.ToString()))]
        };
    }

    public static Beverage ToEntity(this DTOEntities.BeverageOrder beverageDto)
    {
        var beverageSize = MapBeverageSize(beverageDto.BeverageSize.ToString());

        return new Beverage
        {
            Name = beverageDto.Name,
            BasePrice = 3.99m,
            Size = beverageSize,
            Temperature = beverageDto.BeverageTemperature.ToEntity()
        };
    }

    private static BeverageSize MapBeverageSize(string size)
    {
        return size switch
        {
            "Small" => BeverageSize.Small,
            "Medium" => BeverageSize.Medium,
            "Large" => BeverageSize.Large,
            _ => throw new ArgumentException($"Unknown beverage size: {size}")
        };
    }

    private static PizzaSize MapPizzaSize(string size)
    {
        return size.ToLowerInvariant() switch
        {
            "small" => PizzaSize.Small,
            "medium" => PizzaSize.Medium,
            "large" => PizzaSize.Large,
            _ => PizzaSize.Medium
        };
    }

    private static PizzaTopping MapPizzaTopping(string topping)
    {
        return topping.ToLowerInvariant() switch
        {
            "cheese" => PizzaTopping.Cheese,
            "pepperoni" => PizzaTopping.Pepperoni,
            "mushrooms" => PizzaTopping.Mushrooms,
            "onions" => PizzaTopping.Onions,
            "sausage" => PizzaTopping.Sausage,
            "bacon" => PizzaTopping.Bacon,
            "extra cheese" => PizzaTopping.ExtraCheese,
            "black olives" => PizzaTopping.BlackOlives,
            "green peppers" => PizzaTopping.GreenPeppers,
            "pineapple" => PizzaTopping.Pineapple,
            "spinach" => PizzaTopping.Spinach,
            _ => throw new ArgumentException($"Unknown pizza topping: {topping}")
        };
    }
}