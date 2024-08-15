using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IOrderService
{
    public Task<IEnumerable<OrderDto>> GetOrdersOfCustomerAsync(Guid customerId, bool trackChanges);
    public Task<OrderDto?> GetOrderByIdAsync(Guid orderId, Guid customerId, bool trackChanges);
    Task<OrderDto> CreateOrderAsync(OrderForCreationDto order, Guid customerId);
}