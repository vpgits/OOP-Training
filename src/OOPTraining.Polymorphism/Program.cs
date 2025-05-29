using OOPTraining.Common.Services;
using OOPTraining.Polymorphism.Bad.Services;
using OOPTraining.Polymorphism.Good.Services;

using BadEntities = OOPTraining.Polymorphism.Bad.Entities;
using GoodAbstractions = OOPTraining.Polymorphism.Good.Abstractions;
using GoodEntities = OOPTraining.Polymorphism.Good.Entities;

var orderService = new OrderService(new ConsoleInputService(), new ConsoleOutputService());
var typeCheckingService = new TypeCheckingOrderService();
var polymorphicService = new PolymorphicOrderService();
Console.WriteLine("=== Polymorphism Demo ===\n");

var order = orderService.TakeOrder();

Console.WriteLine("\n--- BAD: Type Checking Approach ---");
var badItems = new List<BadEntities.MenuItem>();

if (order.PizzaOrder != null)
{
    badItems.Add(new BadEntities.Pizza
    {
        BasePrice = 12.99m,
        Toppings = [.. order.PizzaOrder.Toppings.Select(t => t.ToEntity())],
        Size = order.PizzaOrder.Size.ToEntity()
    });
}

if (order.BeverageOrder != null)
{
    badItems.Add(new BadEntities.Beverage
    {
        Name = order.BeverageOrder.Name,
        BasePrice = 3.99m,
        Size = order.BeverageOrder.BeverageSize.ToEntity(),
        Temperature = order.BeverageOrder.BeverageTemperature.ToEntity()
    });
}

Console.WriteLine($"Total (Type Checking): ${typeCheckingService.CalculateTotalPrice(badItems):F2}");
var availableBad = typeCheckingService.GetAvailableItemNames(badItems);
Console.WriteLine($"Available Items: {string.Join(", ", availableBad)}");

Console.WriteLine("\n--- GOOD: Polymorphic Approach ---");
var goodItems = new List<GoodAbstractions.IOrderable>();

if (order.PizzaOrder != null)
{
    goodItems.Add(new GoodEntities.Pizza
    {
        BasePrice = 12.99m,
        Toppings = order.PizzaOrder.Toppings.Select(t => t.ToEntity()).ToList(),
        Size = order.PizzaOrder.Size.ToEntity()
    });
}

if (order.BeverageOrder != null)
{
    goodItems.Add(new GoodEntities.Beverage
    {
        Name = order.BeverageOrder.Name,
        BasePrice = 3.99m,
        Size = order.BeverageOrder.BeverageSize.ToEntity(),
        Temperature = order.BeverageOrder.BeverageTemperature.ToEntity()
    });
}

Console.WriteLine($"Total (Polymorphic): ${polymorphicService.CalculateTotalPrice(goodItems):F2}");
var availableGood = polymorphicService.GetAvailableItemNames(goodItems);
Console.WriteLine($"Available Items: {string.Join(", ", availableGood)}");

Console.WriteLine("\n--- Comparison ---");
Console.WriteLine("Bad: Uses type checking (if/else, instanceof)");
Console.WriteLine("Good: Uses polymorphism (virtual methods, interfaces)");

Console.WriteLine("\n=== Final Receipt ===");
orderService.ShowReceipt(order);