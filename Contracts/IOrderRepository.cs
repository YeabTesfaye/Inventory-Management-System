using Entities.Models;

namespace Contracts;

public interface IOrderRepository
{

    IEnumerable<Order> GetOrdersOfCustomer(Guid customerId, bool trackChanges);
    Order? GetOrderById(Guid orderId, bool trackChanges);
    void CreateOrder(Order order);

}