using OOPTraining.Common.Abstractions;
using OOPTraining.Common.Abstractions.Data;

namespace OOPTraining.Common.Services;

public class PizzaOrderService : IOrderService<PizzaOrder>
{
    private readonly IInputService<string> _inputService;
    private readonly IOutputService<string> _outputService;

    public PizzaOrderService(IInputService<string> inputService, IOutputService<string> outputService)
    {
        _inputService = inputService;
        _outputService = outputService;
    }

    public PizzaOrder TakeOrder()
    {
        try
        {
            _outputService.DisplayOutput("=== Welcome to Pizza Palace ===");


            var customerName = GetValidInput(
                "Enter customer name: ",
                input => !string.IsNullOrWhiteSpace(input),
                "Customer name cannot be empty."
            );


            var size = GetValidEnum<OrderPizzaSize>(
                $"Enter pizza size ({string.Join(", ", Enum.GetNames<OrderPizzaSize>())}): "
            );


            var toppings = GetValidToppings();

            var order = new PizzaOrder
            {
                CustomerName = customerName.Trim(),
                Size = size,
                Toppings = toppings
            };

            _outputService.DisplayOutput("Order created successfully!");
            return order;
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to create order", ex);
            throw;
        }
    }

    public void ShowReceipt(PizzaOrder order)
    {
        try
        {
            _outputService.DisplayOutput("\n=== PIZZA ORDER RECEIPT ===");
            _outputService.DisplayOutput($"Customer: {order.CustomerName}");
            _outputService.DisplayOutput($"Size: {order.Size} Pizza");

            if (order.Toppings.Any())
            {
                var toppingsDisplay = string.Join(", ", order.Toppings.Select(FormatToppingName));
                _outputService.DisplayOutput($"Toppings: {toppingsDisplay}");
            }
            else
            {
                _outputService.DisplayOutput("Toppings: None (Plain Pizza)");
            }

            _outputService.DisplayOutput($"Items: {order.Toppings.Count + 1} (1 pizza + {order.Toppings.Count} toppings)");
            _outputService.DisplayOutput("================================");
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to display receipt", ex);
        }
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

    private string FormatToppingName(OrderPizzaTopping topping)
    {
        return topping switch
        {
            OrderPizzaTopping.ExtraCheese => "Extra Cheese",
            OrderPizzaTopping.BlackOlives => "Black Olives",
            OrderPizzaTopping.GreenPeppers => "Green Peppers",
            _ => topping.ToString()
        };
    }
}