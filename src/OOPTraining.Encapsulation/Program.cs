using OOPTraining.Common.Abstractions.Data;
using OOPTraining.Encapsulation.Models.Good;
using OOPTraining.Encapsulation.Models.Bad;
using OOPTraining.Common.Services;

Console.WriteLine("=== OOP Training: Encapsulation Demo ===\n");

var orderService = new OrderService(new ConsoleInputService(), new ConsoleOutputService());
var fullOrder = orderService.TakeOrder();

if (fullOrder.PizzaOrder == null)
{
    Console.WriteLine("No pizza order to demonstrate encapsulation.");
    return;
}

DemonstrateGoodEncapsulation(fullOrder);
DemonstrateBadEncapsulation(fullOrder);

orderService.ShowReceipt(fullOrder);

static void DemonstrateGoodEncapsulation(FullOrder fullOrder)
{
    Console.WriteLine("=== GOOD Encapsulation ===");

    var customer = new Customer(fullOrder.Customer.Name, fullOrder.Customer.Email);
    var pizza = new Pizza(fullOrder.PizzaOrder!.Size.ToEntity());

    foreach (var topping in fullOrder.PizzaOrder.Toppings)
    {
        pizza.AddTopping(topping.ToEntity());
    }

    Console.WriteLine($"Customer: {customer.Name}");
    Console.WriteLine($"Pizza: {pizza}");
    Console.WriteLine($"Price: ${pizza.Price:F2}");
    Console.WriteLine("Data is protected from external modification");
}

static void DemonstrateBadEncapsulation(FullOrder fullOrder)
{
    Console.WriteLine("\n=== BAD Encapsulation ===");

    var badPizza = new BadPizza();
    badPizza.customerName = fullOrder.Customer.Name;
    badPizza.size = fullOrder.PizzaOrder!.Size.ToString().ToLower();

    foreach (var topping in fullOrder.PizzaOrder.Toppings)
    {
        badPizza.AddTopping(topping.ToString());
    }

    badPizza.CalculatePrice();
    Console.WriteLine($"Customer: {badPizza.customerName}");
    Console.WriteLine($"Pizza: {badPizza.size} with {badPizza.toppings.Count} toppings");
    Console.WriteLine($"Price: ${badPizza.price:F2}");

    badPizza.price = -100;
    badPizza.size = "INVALID";
    badPizza.GetToppings().Add("Poison");

    Console.WriteLine("After corruption:");
    Console.WriteLine($"Price: ${badPizza.price:F2} (negative!)");
    Console.WriteLine($"Size: {badPizza.size} (invalid!)");
    Console.WriteLine($"Toppings: {badPizza.toppings.Count} (corrupted!)");
}