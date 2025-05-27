using OOPTraining.Common.Abstractions.Data;

namespace OOPTraining.Common.Abstractions;

public interface IOrderService
{
    FullOrder TakeOrder();
    OrderCustomer TakeCustomerInfo();
    void ShowReceipt(FullOrder order);
}