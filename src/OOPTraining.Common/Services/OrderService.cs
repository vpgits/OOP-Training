using OOPTraining.Common.Abstractions;
using OOPTraining.Common.Abstractions.Data;
using OOPTraining.Common.Entities;

namespace OOPTraining.Common.Services;

public class OrderService(IInputService<string> inputService, IOutputService<string> outputService) : IOrderService
{
    private readonly IInputService<string> _inputService = inputService;
    private readonly IOutputService<string> _outputService = outputService;

    public FullOrder TakeOrder()
    {
        try
        {
            _outputService.DisplayOutput("=== Welcome to Food Palace ===");

            var customer = TakeCustomerInfo();
            var orderType = GetOrderType();

            var fullOrder = new FullOrder { Customer = customer };

            switch (orderType)
            {
                case OrderType.Pizza:
                    fullOrder = fullOrder with { PizzaOrder = TakePizzaOrder() };
                    break;
                case OrderType.Beverage:
                    fullOrder = fullOrder with { BeverageOrder = TakeBeverageOrder() };
                    break;
                case OrderType.Combo:
                    fullOrder = fullOrder with
                    {
                        PizzaOrder = TakePizzaOrder(),
                        BeverageOrder = TakeBeverageOrder()
                    };
                    break;
            }

            _outputService.DisplayOutput("Order created successfully!");
            return fullOrder;
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to create order", ex);
            throw;
        }
    }

    public OrderCustomer TakeCustomerInfo()
    {
        try
        {
            _outputService.DisplayOutput("\n--- Customer Information ---");

            var name = GetValidInput(
                "Enter customer name: ",
                input => !string.IsNullOrWhiteSpace(input),
                "Customer name cannot be empty."
            );

            var email = GetValidInput(
                "Enter customer email: ",
                input => !string.IsNullOrWhiteSpace(input) && input.Contains('@'),
                "Please enter a valid email address."
            );

            var phone = GetValidInput(
                "Enter customer phone: ",
                input => !string.IsNullOrWhiteSpace(input),
                "Customer phone cannot be empty."
            );

            return new OrderCustomer
            {
                Name = name.Trim(),
                Email = email.Trim(),
                Phone = phone.Trim()
            };
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to collect customer info", ex);
            throw;
        }
    }

    public void ShowReceipt(FullOrder order)
    {
        try
        {
            _outputService.DisplayOutput("\n=== FOOD PALACE RECEIPT ===");
            _outputService.DisplayOutput($"Customer: {order.Customer.Name}");
            _outputService.DisplayOutput($"Email: {order.Customer.Email}");
            _outputService.DisplayOutput($"Phone: {order.Customer.Phone}");
            _outputService.DisplayOutput("--------------------------------");

            if (order.PizzaOrder != null)
            {
                ShowPizzaDetails(order.PizzaOrder);
            }

            if (order.BeverageOrder != null)
            {
                ShowBeverageDetails(order.BeverageOrder);
            }

            var itemCount = GetTotalItemCount(order);
            _outputService.DisplayOutput($"Total Items: {itemCount}");
            _outputService.DisplayOutput("================================");
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to display receipt", ex);
        }
    }

    private OrderType GetOrderType()
    {
        _outputService.DisplayOutput("\n--- What would you like to order? ---");
        _outputService.DisplayOutput("1. Pizza only");
        _outputService.DisplayOutput("2. Beverage only");
        _outputService.DisplayOutput("3. Pizza and Beverage combo");

        while (true)
        {
            var input = _inputService.GetInput("Enter your choice (1-3): ");

            switch (input?.Trim())
            {
                case "1":
                    return OrderType.Pizza;
                case "2":
                    return OrderType.Beverage;
                case "3":
                    return OrderType.Combo;
                default:
                    _outputService.DisplayError("Please enter 1, 2, or 3");
                    break;
            }
        }
    }

    private PizzaOrder TakePizzaOrder()
    {
        _outputService.DisplayOutput("\n--- Pizza Order ---");

        var size = GetValidEnum<OrderPizzaSize>(
            $"Enter pizza size ({string.Join(", ", Enum.GetNames<OrderPizzaSize>())}): "
        );

        var toppings = GetValidToppings();

        return new PizzaOrder
        {
            Size = size,
            Toppings = toppings
        };
    }

    private BeverageOrder TakeBeverageOrder()
    {
        _outputService.DisplayOutput("\n--- Beverage Order ---");

        var type = GetValidInput(
            "Enter beverage type (Coke, Pepsi, Water, Coffee, Tea): ",
            input => !string.IsNullOrWhiteSpace(input),
            "Beverage type cannot be empty."
        );

        var size = GetValidInput(
            "Enter beverage size (Small, Medium, Large): ",
            input => !string.IsNullOrWhiteSpace(input),
            "Beverage size cannot be empty."
        );

        var temperature = GetValidInput(
            "Enter beverage temperature (Hot, Cold, Iced): ",
            input => !string.IsNullOrWhiteSpace(input),
            "Beverage temperature cannot be empty."
        );

        return new BeverageOrder
        {
            Name = type.Trim(),
            BeverageSize = (OrderBeverageSize)(Enum.TryParse<OrderBeverageSize>(size, true, out var beverageSize) ? beverageSize : OrderBeverageSize.Medium),
            BeverageTemperature = (OrderBeverageTemperature)(Enum.TryParse<OrderBeverageTemperature>(temperature, true, out var beverageTemp) ? beverageTemp : OrderBeverageTemperature.Hot)
        };
    }

    private void ShowPizzaDetails(PizzaOrder pizza)
    {
        _outputService.DisplayOutput($"Pizza Size: {pizza.Size}");

        if (pizza.Toppings.Any())
        {
            var toppingsDisplay = string.Join(", ", pizza.Toppings.Select(t => t.ToString()));
            _outputService.DisplayOutput($"Pizza Toppings: {toppingsDisplay}");
        }
        else
        {
            _outputService.DisplayOutput("Pizza Toppings: None (Plain Pizza)");
        }
    }

    private void ShowBeverageDetails(BeverageOrder beverage)
    {
        _outputService.DisplayOutput($"Beverage: {beverage.Name} {beverage.BeverageSize}");
        _outputService.DisplayOutput($"Temperature: {beverage.BeverageTemperature}");
    }

    private int GetTotalItemCount(FullOrder order)
    {
        var count = 0;

        if (order.PizzaOrder != null)
        {
            count += 1 + order.PizzaOrder.Toppings.Count; // 1 pizza + toppings
        }

        if (order.BeverageOrder != null)
        {
            count += 1; // 1 beverage
        }

        return count;
    }

    private string GetValidInput(string prompt, Func<string, bool> validator, string errorMessage)
    {
        while (true)
        {
            var input = _inputService.GetInput(prompt);

            if (validator(input))
            {
                return input;
            }

            _outputService.DisplayError(errorMessage);
        }
    }

    private T GetValidEnum<T>(string prompt) where T : struct, Enum
    {
        while (true)
        {
            var input = _inputService.GetInput(prompt);

            if (Enum.TryParse<T>(input, true, out var result))
            {
                return result;
            }

            var validValues = string.Join(", ", Enum.GetNames<T>());
            _outputService.DisplayError($"Invalid input. Please enter one of: {validValues}");
        }
    }

    private bool GetYesNoInput(string prompt)
    {
        while (true)
        {
            var input = _inputService.GetInput(prompt);

            switch (input?.Trim().ToLowerInvariant())
            {
                case "y":
                case "yes":
                    return true;
                case "n":
                case "no":
                    return false;
                default:
                    _outputService.DisplayError("Please enter 'y' for yes or 'n' for no");
                    break;
            }
        }
    }

    private List<OrderPizzaTopping> GetValidToppings()
    {
        var availableToppings = Enum.GetNames<OrderPizzaTopping>();
        _outputService.DisplayOutput($"Available toppings: {string.Join(", ", availableToppings)}");

        var toppingsInput = _inputService.GetInput("Enter toppings (comma separated, or press Enter for none): ");

        if (string.IsNullOrWhiteSpace(toppingsInput))
        {
            return new List<OrderPizzaTopping>();
        }

        var toppings = new List<OrderPizzaTopping>();
        var inputs = toppingsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var input in inputs)
        {
            var trimmedInput = input.Trim();
            if (Enum.TryParse<OrderPizzaTopping>(trimmedInput, true, out var topping))
            {
                if (!toppings.Contains(topping))
                {
                    toppings.Add(topping);
                }
            }
            else
            {
                _outputService.DisplayError($"Unknown topping '{trimmedInput}' - skipping");
            }
        }

        return toppings;
    }
}

