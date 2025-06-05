public interface IOrderService<T>
    where T : class
{
    decimal CalculateTotalPrice(List<T> items);
    string GetDisplayText(T item);
    bool IsItemAvailable(T item);
}