namespace OOPTraining.Encapsulation.Models.Good;

public class Customer
{
    private string name = string.Empty;
    private string email = string.Empty;

    public string Name
    {
        get => name;
        set => name = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Name cannot be empty")
            : value;
    }

    public string Email
    {
        get => email;
        set => email = IsValidEmail(value)
            ? value
            : throw new ArgumentException("Invalid email");
    }

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }

    private static bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains('@');
    }
}