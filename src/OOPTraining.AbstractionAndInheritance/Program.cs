using BadAbstractions = OOPTraining.AbstractionAndInheritance.Bad.Abstractions;
using BadEntities = OOPTraining.AbstractionAndInheritance.Bad.Entities;
using GoodEntities = OOPTraining.AbstractionAndInheritance.Good.Entities;
using GoodServices = OOPTraining.AbstractionAndInheritance.Good.Services;
using OOPTraining.Common.Abstractions.Data;
using OOPTraining.Common.Services;
using OOPTraining.Common.Entities;

Console.WriteLine("=== OOP Training: Abstraction and Inheritance ===\n");

var inputService = new ConsoleInputService();
var outputService = new ConsoleOutputService();
var orderService = new OrderService(inputService, outputService);

var fullOrder = orderService.TakeOrder();

RunBadImplementation(fullOrder);
RunGoodImplementation(fullOrder);

orderService.ShowReceipt(fullOrder);

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();


static void RunBadImplementation(FullOrder fullOrder)
{
    Console.WriteLine("\n=== BAD Implementation ===");

    if (fullOrder.PizzaOrder == null) return;

    var customer = new BadEntities.Customer(fullOrder.Customer.Name, fullOrder.Customer.Email);
    var pizza = new BadEntities.Pizza(fullOrder.PizzaOrder.Size.ToEntity(), "Custom Pizza");

    customer.CustomizePizza(pizza, fullOrder.PizzaOrder.Toppings.Select(t => t.ToEntity()).ToList());

    var beverage = new BadEntities.Beverage("Coke", BeverageSize.Large, BeverageTemperature.Cold);

    var menuItems = new List<BadAbstractions.IMenuItem> { pizza, beverage };

    Console.WriteLine("Kitchen sink interface forces inappropriate methods:");
    Console.WriteLine($"Beverage add pepperoni: {beverage.AddTopping(PizzaTopping.Pepperoni)}");
    Console.WriteLine($"Beverage toppings: {beverage.GetToppings().Count}");

    Console.WriteLine("Static god class with type checking:");
    foreach (var item in menuItems)
    {
        Console.WriteLine($"{item.GetType().Name}: {BadEntities.MenuManager.GetItemDescription(item)}");
    }

    Console.WriteLine($"Tight coupling - customer calculates total: ${customer.GetTotalCost(menuItems):F2}");
}

static void RunGoodImplementation(FullOrder fullOrder)
{
    Console.WriteLine("\n=== GOOD Implementation ===");

    var pricingService = new GoodServices.PricingService();
    var order = CreateGoodOrder(fullOrder);

    Console.WriteLine($"Clean entity hierarchy:");
    Console.WriteLine($"Customer: {order.Customer.Name}");
    Console.WriteLine($"Items: {order.Items.Count}");

    foreach (var item in order.Items)
    {
        Console.WriteLine($"  {item.Name}: ${item.CalculatePrice():F2}");
    }

    Console.WriteLine($"Proper responsibility separation:");
    Console.WriteLine($"Order total: ${order.CalculateTotal():F2}");
    Console.WriteLine($"Service total: ${pricingService.CalculateOrderTotal(order):F2}");
    Console.WriteLine($"Order status: {order.Status}");
}

static GoodEntities.Order CreateGoodOrder(FullOrder fullOrder)
{
    var order = new GoodEntities.Order();

    order.Customer = new GoodEntities.Customer
    {
        Name = fullOrder.Customer.Name,
        Email = fullOrder.Customer.Email,
        Phone = fullOrder.Customer.Phone ?? string.Empty
    };

    if (fullOrder.PizzaOrder != null)
    {
        var pizza = new GoodEntities.Pizza
        {
            Name = "Custom Pizza",
            BasePrice = 12.99m,
            Size = fullOrder.PizzaOrder.Size.ToEntity(),
            Toppings = [.. fullOrder.PizzaOrder.Toppings.Select(t => t.ToEntity())]
        };
        order.AddItem(pizza);
    }

    if (fullOrder.BeverageOrder != null)
    {
        var beverage = new GoodEntities.Beverage
        {
            Name = fullOrder.BeverageOrder.Name,
            BasePrice = 3.99m,
            Size = fullOrder.BeverageOrder.BeverageSize.ToEntity(),
            Temperature = fullOrder.BeverageOrder.BeverageTemperature.ToEntity()
        };
        order.AddItem(beverage);
    }

    return order;
}


