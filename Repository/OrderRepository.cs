using Contracts;
using Entities.Models;

namespace Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public Order GetOrder(Guid orderId)
    => FindByCondition(o => o.OrderId == orderId,trackChanges:false).FirstOrDefault();

    public IEnumerable<Order> GetOrders(bool trackChanges)
    => [.. FindByCondition(o => true, trackChanges).OrderBy(c => c.OrderDate)];
}