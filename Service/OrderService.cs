using AutoMapper;
using Contracts;
using Entities.Exceptions;
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

    public OrderDto? GetOrderById(Guid orderId, bool trackChanges)
    {
        var order = _repositoryManager.Order.GetOrderById(orderId,trackChanges)
         ?? throw new OrderNotFoundException(orderId);
        var orderDto = _mapper.Map<OrderDto>(order);
        return orderDto;
    }

    public IEnumerable<OrderDto> GetOrdersOfCustomer(Guid customerId, bool trackChanges)
    {
        var orders = _repositoryManager.Order.GetOrdersOfCustomer(customerId, trackChanges);
        var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return ordersDto;
    }


}