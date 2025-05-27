using OOPTraining.Common.Abstractions;

namespace OOPTraining.Common.Services;

public class ConsoleOutputService : IOutputService<string>
{
    public void DisplayOutput(string output)
    {
        Console.WriteLine(output);
    }

    public void DisplayOutput(string message, string output)
    {
        Console.WriteLine($"{message}: {output}");
    }

    public void DisplayError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {errorMessage}");
        Console.ResetColor();
    }

    public void DisplayError(string errorMessage, Exception exception)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {errorMessage}");
        Console.WriteLine($"Exception: {exception.Message}");
        Console.ResetColor();
    }
}