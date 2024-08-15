using Entities.Models;

namespace Contracts;

public interface IOrderRepository
{

    Task<IEnumerable<Order>> GetOrdersOfCustomerAsync(Guid customerId, bool trackChanges);
    Task<Order?> GetOrderByIdAsync(Guid orderId, bool trackChanges);

    void CreateOrder(Order order);

}