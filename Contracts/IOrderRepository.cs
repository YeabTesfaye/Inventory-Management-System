using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IOrderRepository
{

    Task<PagedList<Order>> GetOrdersOfCustomerAsync(Guid customerId, OrderParameters orderParameters,bool trackChanges);
    Task<Order?> GetOrderByIdAsync(Guid orderId, bool trackChanges);

    void CreateOrder(Order order);
    void DeleteOrder(Order order);

}