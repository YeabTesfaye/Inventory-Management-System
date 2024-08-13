using Entities.Models;

namespace Contracts;

public interface IOrderRepository
{

    IEnumerable<Order> GetOrders(bool trackChanges);
    Order GetOrder(Guid orderId);

}