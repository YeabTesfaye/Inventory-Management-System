using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public OrderService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public OrderDto GetOrder(Guid orderId)
    {
        var order = _repositoryManager.Order.GetOrder(orderId);
        var orderDto = _mapper.Map<OrderDto>(order);
        return orderDto;
    }

    public IEnumerable<OrderDto> GetOrders()
    {
        var orders = _repositoryManager.Order.GetOrders(trackChanges: false);
        var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return ordersDto;
    }


}