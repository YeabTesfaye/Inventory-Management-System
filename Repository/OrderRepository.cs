using Contracts;
using Entities.Models;

namespace Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateOrder(Order order) => Create(order);

    public Order? GetOrderById(Guid orderId, bool trackChanges)
    => FindByCondition(o => o.OrderId == orderId, trackChanges).FirstOrDefault();

    public IEnumerable<Order> GetOrdersOfCustomer(Guid customerId, bool trackChanges)
    => [.. FindByCondition(o => o.CustomerId == customerId, trackChanges).OrderBy(o => o.OrderDate)];
}