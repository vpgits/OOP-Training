using OOPTraining.Common.Abstractions;

namespace OOPTraining.Common.Services;

public class ConsoleInputService : IInputService<string>
{
    public string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? throw new ArgumentNullException(nameof(prompt), "Input cannot be null.");
    }

    public string GetInput(string prompt, Func<string, string> parseFunc)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be empty.");
        }
        return parseFunc(input);
    }
}