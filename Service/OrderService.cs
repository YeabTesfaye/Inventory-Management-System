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
    private async Task CheckIfCustomerExists(Guid customerId, bool trackChanges)
    {
        _ = await _repositoryManager.Customer.GetCustomerAsync(customerId, trackChanges)
        ?? throw new CustomerNotFoundException(customerId);
    }


}