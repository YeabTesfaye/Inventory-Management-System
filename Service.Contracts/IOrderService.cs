using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IOrderService
{
    public IEnumerable<OrderDto> GetOrders();
    public OrderDto GetOrder(Guid orderId);
}