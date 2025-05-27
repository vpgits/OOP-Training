namespace OOPTraining.Common.Abstractions;

public interface IOrderService<T> where T : class
{
    T TakeOrder();
    void ShowReceipt(T order);
}