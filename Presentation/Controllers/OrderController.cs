using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;
[Route("api/customers/{customerId}/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public OrderController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetOrdersOfCustomer(Guid customerId)
    {
        var orders = await _serviceManager.OrderService.GetOrdersOfCustomerAsync(customerId, trackChanges: false);
        return Ok(orders);
    }
    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId, [FromRoute] Guid customerId)
    {
        var order = await _serviceManager.OrderService.GetOrderByIdAsync(orderId, customerId, trackChanges: false);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderForCreationDto order, [FromRoute] Guid customerId)
    {
        if (order == null)
            return BadRequest("OrderForCreationDto object is null");

        var createOrder = await _serviceManager.OrderService.CreateOrderAsync(order, customerId);
        return CreatedAtAction(nameof(GetOrderById), new { orderId = createOrder.OrderId, customerId }, createOrder);
    }


}