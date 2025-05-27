using OOPTraining.Common.Abstractions.Data;
using OOPTraining.Encapsulation.Models.Good;
using OOPTraining.Encapsulation.Models.Bad;
using OOPTraining.Common.Services;
using OOPTraining.Common.Entities;



Console.WriteLine("=== OOP Training: Encapsulation Demo ===\n");

var orderService = new PizzaOrderService(new ConsoleInputService(), new ConsoleOutputService());

// Use new API to collect both customer info and order
var fullOrder = orderService.TakeOrderWithCustomer();

Console.WriteLine("\n" + "=".PadRight(50, '='));
Console.WriteLine("COMPARING ENCAPSULATION APPROACHES");
Console.WriteLine("=".PadRight(50, '='));

// Pass both customer and order data to demonstrations
DemonstrateGoodEncapsulation(fullOrder.Customer, fullOrder.Order);

Console.WriteLine();

DemonstrateBadEncapsulation(fullOrder.Customer, fullOrder.Order);

// Show enhanced receipt with customer info
Console.WriteLine("\n" + "=".PadRight(50, '='));
Console.WriteLine("FINAL ORDER RECEIPT");
Console.WriteLine("=".PadRight(50, '='));
orderService.ShowFullReceipt(fullOrder);


static void DemonstrateGoodEncapsulation(OrderCustomer orderCustomer, PizzaOrder order)
{
    Console.WriteLine("✅ GOOD ENCAPSULATION EXAMPLE:");
    Console.WriteLine("--------------------------------");

    // Create customer using actual customer data from order service
    var customer = new Customer(orderCustomer.Name, orderCustomer.Email);
    Console.WriteLine($"Customer: {customer.Name} ({customer.Email})");

    var goodPizza = new Pizza(ToPizzaSize(order.Size));

    foreach (var orderTopping in order.Toppings)
    {
        var success = goodPizza.AddTopping(ToPizzaTopping(orderTopping));
        Console.WriteLine($"Adding {orderTopping}: {(success ? "✅ Success" : "❌ Failed")}");
    }

    Console.WriteLine($"Final Pizza: {goodPizza}");
    Console.WriteLine($"Price calculated automatically: ${goodPizza.Price:F2}");

    /*
    // goodPizza.Size = PizzaSize.Small;           // ❌ COMPILE ERROR: readonly property
    // goodPizza._toppings.Add("Hacker");          // ❌ COMPILE ERROR: private field  
    // goodPizza.Toppings.Add("Virus");            // ❌ COMPILE ERROR: readonly collection
    // customer.Name = "Hacker";                   // ❌ COMPILE ERROR: private setter
    */

    Console.WriteLine("🔒 Data is protected from external modification!");
}

static void DemonstrateBadEncapsulation(OrderCustomer orderCustomer, PizzaOrder order)
{
    Console.WriteLine("❌ BAD ENCAPSULATION EXAMPLE:");
    Console.WriteLine("-------------------------------");

    var badPizza = new BadPizza();
    badPizza.customerName = orderCustomer.Name;  // Use actual customer name
    badPizza.size = order.Size.ToString().ToLower();

    foreach (var orderTopping in order.Toppings)
    {
        badPizza.AddTopping(orderTopping.ToString());
        Console.WriteLine($"Added {orderTopping} (no validation)");
    }

    badPizza.CalculatePrice();

    Console.WriteLine($"Pizza created with manual steps");
    badPizza.PrintInfo();

    Console.WriteLine("\n🚨 DEMONSTRATING DANGERS:");

    badPizza.price = -100;
    Console.WriteLine($"Someone set price to: ${badPizza.price} (negative price!)");

    badPizza.size = "GIGANTIC";
    Console.WriteLine($"Someone set size to: {badPizza.size} (invalid size!)");

    var toppings = badPizza.GetToppings();
    toppings.Add("Poison");
    toppings.Add("Glass");
    Console.WriteLine($"Someone added dangerous toppings: {string.Join(", ", toppings)}");

    toppings.Clear();
    Console.WriteLine($"Oops! All toppings cleared. Remaining: {badPizza.toppings.Count}");

    Console.WriteLine("🔓 No protection from data corruption!");
}

static PizzaSize ToPizzaSize(OrderPizzaSize orderSize)
{
    return orderSize switch
    {
        OrderPizzaSize.Small => PizzaSize.Small,
        OrderPizzaSize.Medium => PizzaSize.Medium,
        OrderPizzaSize.Large => PizzaSize.Large,
        _ => PizzaSize.Medium
    };
}

static PizzaTopping ToPizzaTopping(OrderPizzaTopping orderTopping)
{
    return orderTopping switch
    {
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
        _ => PizzaTopping.ExtraCheese
    };
}
