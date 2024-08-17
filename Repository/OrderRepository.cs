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
        // Apply filtering based on OrderStatus if it's not empty
        var orders = await FindByCondition(o => o.CustomerId == customerId &&
                                                (string.IsNullOrEmpty(orderParameters.OrderStatus) || o.OrderStatus.Contains(orderParameters.OrderStatus)),
                                                trackChanges)
            .OrderBy(o => o.OrderDate)  // Ensure orders are ordered by OrderDate
            .ToListAsync();

        // Paginate the filtered results
        return PagedList<Order>
            .ToPagedList(orders, orderParameters.PageNumber, orderParameters.PageSize);
    }



}

