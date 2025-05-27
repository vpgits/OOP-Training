namespace OOPTraining.Common.Abstractions;

public interface IInputService<T> where T : class
{
    T GetInput(string prompt);
    T GetInput(string prompt, Func<string, T> parseFunc);
}