using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IOrderService
{
    public IEnumerable<OrderDto> GetOrdersOfCustomer(Guid customerId, bool trackChanges);
    public OrderDto? GetOrderById(Guid orderId,bool trackChanges);
}