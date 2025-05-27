using OOPTraining.Common.Entities;
using OOPTraining.Common.Abstractions.Data;

public static class MappingExtensions
{
    // Mapping extension for OrderPizzaSize to PizzaSize
    public static PizzaSize ToEntity(this OrderPizzaSize dto)
    {
        return dto switch
        {
            OrderPizzaSize.Small => PizzaSize.Small,
            OrderPizzaSize.Medium => PizzaSize.Medium,
            OrderPizzaSize.Large => PizzaSize.Large,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, null)
        };
    }

    // Mapping extension for OrderPizzaTopping to PizzaTopping
    public static PizzaTopping ToEntity(this OrderPizzaTopping dto)
    {
        return dto switch
        {
            OrderPizzaTopping.Cheese => PizzaTopping.Cheese,
            OrderPizzaTopping.Pepperoni => PizzaTopping.Pepperoni,
            OrderPizzaTopping.Mushrooms => PizzaTopping.Mushrooms,
            OrderPizzaTopping.Onions => PizzaTopping.Onions,
            OrderPizzaTopping.Sausage => PizzaTopping.Sausage,
            OrderPizzaTopping.Bacon => PizzaTopping.Bacon,
            OrderPizzaTopping.ExtraCheese => PizzaTopping.ExtraCheese,
            OrderPizzaTopping.BlackOlives => PizzaTopping.BlackOlives,
            OrderPizzaTopping.GreenPeppers => PizzaTopping.GreenPeppers,
            OrderPizzaTopping.Pineapple => PizzaTopping.Pineapple,
            OrderPizzaTopping.Spinach => PizzaTopping.Spinach,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, null)
        };
    }

    // Mapping extension for OrderBeverageSize to BeverageSize
    public static BeverageSize ToEntity(this OrderBeverageSize dto)
    {
        return dto switch
        {
            OrderBeverageSize.Small => BeverageSize.Small,
            OrderBeverageSize.Medium => BeverageSize.Medium,
            OrderBeverageSize.Large => BeverageSize.Large,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, null)
        };
    }

    // Mapping extension for OrderBeverageTemperature to BeverageTemperature
    public static BeverageTemperature ToEntity(this OrderBeverageTemperature dto)
    {
        return dto switch
        {
            OrderBeverageTemperature.Cold => BeverageTemperature.Cold,
            OrderBeverageTemperature.Hot => BeverageTemperature.Hot,
            OrderBeverageTemperature.Iced => BeverageTemperature.Iced,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, null)
        };
    }
}