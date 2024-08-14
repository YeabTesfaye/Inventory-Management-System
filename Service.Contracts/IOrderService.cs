using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IOrderService
{
    public IEnumerable<OrderDto> GetOrdersOfCustomer(Guid customerId, bool trackChanges);
    public OrderDto? GetOrderById(Guid orderId,Guid customerId,bool trackChanges);
    OrderDto CreateOrder(OrderForCreationDto order, Guid customerId);
}