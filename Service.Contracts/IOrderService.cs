using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IOrderService
{
    public Task<IEnumerable<OrderDto>> GetOrdersOfCustomerAsync(Guid customerId,OrderParameters orderParameters, bool trackChanges);
    public Task<OrderDto?> GetOrderByIdAsync(Guid orderId, Guid customerId, bool trackChanges);
    Task<OrderDto> CreateOrderAsync(OrderForCreationDto order, Guid customerId);
    Task DeleteOrderAsync(Guid orderId);
    Task UpdateOrder(Guid customerId, Guid itemId, OrderForUpdateDto order, bool trackChanges);
}