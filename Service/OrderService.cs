using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
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

    public OrderDto CreateOrder(OrderForCreationDto order, Guid customerId)
    {
        CheckIfCustomerExists(customerId, trackChanges: false);
        var createEntity = _mapper.Map<Order>(order);
        _repositoryManager.Order.CreateOrder(createEntity);
        _repositoryManager.Save();
        var orderToReturn = _mapper.Map<OrderDto>(createEntity);
        return orderToReturn;
    }

    public OrderDto? GetOrderById(Guid orderId, Guid customerId,bool trackChanges)
    {
        CheckIfCustomerExists(customerId,trackChanges:false);
        var order = _repositoryManager.Order.GetOrderById(orderId, trackChanges)
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
    private void CheckIfCustomerExists(Guid customerId, bool trackChanges)
    {
        _ = _repositoryManager.Customer.GetCustomer(customerId, trackChanges)
        ?? throw new CustomerNotFoundException(customerId);
    }


}