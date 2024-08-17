using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

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
    public async Task<PagedList<Order>> GetOrdersOfCustomerAsync(Guid customerId, OrderParameters orderParameters, bool trackChanges)
    {
        var orders = await FindByCondition(o => o.CustomerId == customerId, trackChanges)
             .OrderBy(o => o.OrderDate)
             .ToListAsync();
        return PagedList<Order>
        .ToPagedList(orders, orderParameters.PageNumber, orderParameters.PageSize);
    }


}

