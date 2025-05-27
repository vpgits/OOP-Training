namespace OOPTraining.Common.Abstractions;

public interface IOutputService<T> where T : class
{
    void DisplayOutput(T output);
    void DisplayOutput(string message, T output);
    void DisplayError(string errorMessage);
    void DisplayError(string errorMessage, Exception exception);
}