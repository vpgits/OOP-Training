using OOPTraining.Common.Entities;
using OOPTraining.Polymorphism.Good.Abstractions;

namespace OOPTraining.Polymorphism.Good.Entities;

public class Beverage : MenuItem, IOrderable
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

    public override string GetDisplayName()
    {
        return $"{Name} ({Size}, {Temperature})";
    }

    public override bool IsAvailable()
    {
        return Temperature != BeverageTemperature.Hot;
    }
}