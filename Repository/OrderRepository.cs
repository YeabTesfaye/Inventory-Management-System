using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateOrder(Order order) => Create(order);

    public void DeleteOrder(Order order) => Delete(order);

    public async Task<Order?> GetOrderByIdAsync(Guid orderId, bool trackChanges)
    => await FindByCondition(o => o.OrderId == orderId, trackChanges).SingleOrDefaultAsync();

    public async Task<IEnumerable<Order>> GetOrdersOfCustomerAsync(Guid customerId, bool trackChanges)
    => await FindByCondition(o => o.CustomerId == customerId, trackChanges).OrderBy(o => o.OrderDate).ToListAsync();
}