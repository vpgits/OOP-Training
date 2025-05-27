using OOPTraining.Common.Abstractions;
using OOPTraining.Common.Abstractions.Data;
using OOPTraining.Common.Services;


var orderService = new PizzaOrderService(new ConsoleInputService(), new ConsoleOutputService());
var orderData = orderService.TakeOrder();


Pizza pizza = new Pizza();


pizza.customerName = orderData.CustomerName;
pizza.size = orderData.Size.ToString().ToLower();
pizza.toppings = new List<string>();


foreach (var topping in orderData.Toppings)
{
    pizza.toppings.Add(topping.ToString());
}


pizza.CalculateEverything();


pizza.price = pizza.price - 1000;
pizza.customerName = "HACKED!";


orderService.ShowReceipt(CreateOrderFromPizza(pizza));


static PizzaOrder CreateOrderFromPizza(Pizza pizza)
{
    var size = Enum.Parse<OrderPizzaSize>(pizza.size, true);
    var toppings = pizza.toppings.Select(t => Enum.Parse<OrderPizzaTopping>(t, true)).ToList();

    return new PizzaOrder
    {
        CustomerName = pizza.customerName,
        Size = size,
        Toppings = toppings
    };
}


public class Pizza
{

    public string customerName;
    public string size;
    public List<string> toppings;
    public double price;
    public string status;


    public Pizza()
    {
        toppings = new List<string>();
        status = "raw";
        Console.WriteLine("Pizza created in constructor!");
    }


    public void CalculateEverything()
    {

        if (size == "small") price = 8.0;
        else if (size == "medium") price = 10.0;
        else if (size == "large") price = 12.0;
        else price = 0;

        price += toppings.Count * 1.5;


        status = "cooked";


        Console.WriteLine($"Cooking {size} pizza for {customerName}");
    }
}