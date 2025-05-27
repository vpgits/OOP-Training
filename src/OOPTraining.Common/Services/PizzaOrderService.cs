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

    // EXISTING API - Maintains backward compatibility
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

    // NEW API - Collects full customer info + pizza order
    public FullPizzaOrder TakeOrderWithCustomer()
    {
        try
        {
            _outputService.DisplayOutput("=== Welcome to Pizza Palace ===");

            // Collect customer information
            var customer = TakeCustomerInfo();

            // Collect pizza order (reuse existing logic but with customer name)
            var order = TakePizzaOrder(customer.Name);

            var result = new FullPizzaOrder(customer, order);
            ;
            _outputService.DisplayOutput("Full order created successfully!");
            return result;
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to create full order", ex);
            throw;
        }
    }

    // NEW API - Just customer info collection (can be used standalone)
    public OrderCustomer TakeCustomerInfo()
    {
        try
        {
            _outputService.DisplayOutput("\n--- Customer Information ---");

            var customerName = GetValidInput(
                "Enter customer name: ",
                input => !string.IsNullOrWhiteSpace(input),
                "Customer name cannot be empty."
            );

            var email = GetValidInput(
                "Enter customer email: ",
                input => !string.IsNullOrWhiteSpace(input) && input.Contains('@'),
                "Please enter a valid email address."
            );

            return new OrderCustomer
            {
                Name = customerName.Trim(),
                Email = email.Trim()
            };
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to collect customer info", ex);
            throw;
        }
    }

    // EXISTING API - Maintains backward compatibility
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

    // NEW API - Enhanced receipt with full customer info
    public void ShowFullReceipt(FullPizzaOrder fullOrder)
    {
        try
        {
            _outputService.DisplayOutput("\n=== PIZZA PALACE RECEIPT ===");
            _outputService.DisplayOutput($"Customer: {fullOrder.Customer.Name}");
            _outputService.DisplayOutput($"Email: {fullOrder.Customer.Email}");
            _outputService.DisplayOutput($"Size: {fullOrder.Order.Size} Pizza");

            if (fullOrder.Order.Toppings.Any())
            {
                var toppingsDisplay = string.Join(", ", fullOrder.Order.Toppings.Select(FormatToppingName));
                _outputService.DisplayOutput($"Toppings: {toppingsDisplay}");
            }
            else
            {
                _outputService.DisplayOutput("Toppings: None (Plain Pizza)");
            }

            _outputService.DisplayOutput($"Items: {fullOrder.Order.Toppings.Count + 1} (1 pizza + {fullOrder.Order.Toppings.Count} toppings)");
            _outputService.DisplayOutput("================================");
        }
        catch (Exception ex)
        {
            _outputService.DisplayError("Failed to display full receipt", ex);
        }
    }

    // PRIVATE HELPER - Extracted pizza order logic
    private PizzaOrder TakePizzaOrder(string customerName)
    {
        _outputService.DisplayOutput("\n--- Pizza Order ---");

        var size = GetValidEnum<OrderPizzaSize>(
            $"Enter pizza size ({string.Join(", ", Enum.GetNames<OrderPizzaSize>())}): "
        );

        var toppings = GetValidToppings();

        return new PizzaOrder
        {
            CustomerName = customerName,
            Size = size,
            Toppings = toppings
        };
    }

    // EXISTING PRIVATE METHODS - Unchanged for compatibility
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