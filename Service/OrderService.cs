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

    public async Task<OrderDto> CreateOrderAsync(OrderForCreationDto order, Guid customerId)
    {
        await CheckIfCustomerExists(customerId, trackChanges: false);
        var createEntity = _mapper.Map<Order>(order);
        _repositoryManager.Order.CreateOrder(createEntity);
        await _repositoryManager.SaveAsync();
        var orderToReturn = _mapper.Map<OrderDto>(createEntity);
        return orderToReturn;
    }

    public async Task DeleteOrderAsync(Guid orderId)
    {
        var order = await CheckIfOrderExistAndReturn(orderId, trackChanges: false);
        _repositoryManager.Order.DeleteOrder(order);
        await _repositoryManager.SaveAsync();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId, Guid customerId, bool trackChanges)
    {
        await CheckIfCustomerExists(customerId, trackChanges: false);
        var order = await _repositoryManager.Order.GetOrderByIdAsync(orderId, trackChanges)
         ?? throw new OrderNotFoundException(orderId);
        var orderDto = _mapper.Map<OrderDto>(order);
        return orderDto;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersOfCustomerAsync(Guid customerId, bool trackChanges)
    {
        var orders = await _repositoryManager.Order.GetOrdersOfCustomerAsync(customerId, trackChanges);
        var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return ordersDto;
    }

    public async Task UpdateOrder(Guid customerId, Guid itemId, OrderForUpdateDto order, bool trackChanges)
    {
        await CheckIfCustomerExists(customerId, trackChanges: false);
        var orderEntity = await CheckIfOrderExistAndReturn(itemId, trackChanges: false);
        Console.WriteLine(orderEntity);
        _mapper.Map(order, orderEntity);
        await _repositoryManager.SaveAsync();
    }

    private async Task CheckIfCustomerExists(Guid customerId, bool trackChanges)
    {
        _ = await _repositoryManager.Customer.GetCustomerAsync(customerId, trackChanges)
        ?? throw new CustomerNotFoundException(customerId);
    }
    private async Task<Order> CheckIfOrderExistAndReturn(Guid orderId, bool trackChanges)
    {
        var order = await _repositoryManager.Order.GetOrderByIdAsync(orderId, trackChanges)
        ?? throw new OrderNotFoundException(orderId);
        return order;
    }


}