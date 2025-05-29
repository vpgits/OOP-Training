using OOPTraining.Common.Entities;

namespace OOPTraining.Polymorphism.Bad.Entities;

public class Beverage : MenuItem
{
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
}