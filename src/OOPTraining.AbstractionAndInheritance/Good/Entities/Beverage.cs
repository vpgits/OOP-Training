using OOPTraining.Common.Entities;

namespace OOPTraining.AbstractionAndInheritance.Good.Entities;

public class Beverage : MenuItem
{
    public override string Name { get; init; } = string.Empty;
    public override decimal BasePrice { get; init; }
    public BeverageSize Size { get; init; }
    public BeverageTemperature Temperature { get; init; }

    public override decimal CalculatePrice()
    {
        var sizeMultiplier = Size switch
        {
            BeverageSize.Small => 1.0m,
            BeverageSize.Medium => 1.3m,
            BeverageSize.Large => 1.6m,
            _ => 1.0m
        };

        return BasePrice * sizeMultiplier;
    }

    public Beverage(string name, decimal basePrice, BeverageSize size, BeverageTemperature temperature)
    {
        Name = name;
        BasePrice = basePrice;
        Size = size;
        Temperature = temperature;
    }
}