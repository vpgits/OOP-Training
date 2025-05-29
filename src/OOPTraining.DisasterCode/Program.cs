using OOPTraining.Common.Services;

var orderService = new OrderService(new ConsoleInputService(), new ConsoleOutputService());
var fullOrder = orderService.TakeOrder();

Pizza pizza = new Pizza();

pizza.customerName = fullOrder.Customer.Name;
pizza.customerEmail = fullOrder.Customer.Email;

if (fullOrder.PizzaOrder != null)
{
    pizza.size = fullOrder.PizzaOrder.Size.ToString().ToLower();
    pizza.toppings = new List<string>();

    foreach (var topping in fullOrder.PizzaOrder.Toppings)
    {
        pizza.toppings.Add(topping.ToString());
    }
}

if (fullOrder.BeverageOrder != null)
{
    pizza.beverage = fullOrder.BeverageOrder.Name;
    pizza.beverageSize = fullOrder.BeverageOrder.BeverageSize.ToString();
}

pizza.CalculateEverything();

pizza.price = pizza.price - 1000;
pizza.customerName = "HACKED!";
pizza.customerEmail = "hacker@evil.com";

orderService.ShowReceipt(fullOrder);

public class Pizza
{
    public string customerName;
    public string customerEmail;
    public string size;
    public List<string> toppings;
    public string beverage;
    public string beverageSize;
    public double price;
    public string status;
    public bool isPaid;

    public Pizza()
    {
        toppings = new List<string>();
        status = "raw";
        isPaid = false;
        Console.WriteLine("Pizza created in constructor!");
    }

    public void CalculateEverything()
    {
        if (size == "small") price = 8.0;
        else if (size == "medium") price = 10.0;
        else if (size == "large") price = 12.0;
        else price = 0;

        price += toppings.Count * 1.5;

        if (beverage != null)
        {
            if (beverageSize == "Small") price += 2.0;
            else if (beverageSize == "Medium") price += 3.0;
            else if (beverageSize == "Large") price += 4.0;
        }

        status = "cooked";
        isPaid = true;

        Console.WriteLine($"Cooking {size} pizza for {customerName}");
        if (beverage != null)
        {
            Console.WriteLine($"Preparing {beverageSize} {beverage}");
        }
    }
}