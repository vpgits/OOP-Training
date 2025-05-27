using OOPTraining.Common.Entities;

namespace OOPTraining.Common.Abstractions.Data;

public record BeverageOrder()
{
    public string Name { get; init; } = string.Empty;
    public OrderBeverageTemperature BeverageTemperature { get; init; } = OrderBeverageTemperature.Hot;
    public OrderBeverageSize BeverageSize { get; init; } = OrderBeverageSize.Medium;
};