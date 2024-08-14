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
    public IActionResult GetOrdersOfCustomer(Guid customerId)
    {
        var orders = _serviceManager.OrderService.GetOrdersOfCustomer(customerId,trackChanges: false);
        return Ok(orders);
    }
    [HttpGet("{orderId:guid}")]
    public IActionResult GetOrderById([FromRoute] Guid orderId)
    {
        var order = _serviceManager.OrderService.GetOrderById(orderId,  trackChanges: false);
        return Ok(order);
    }
    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderForCreationDto order){
        if (order == null)
            return BadRequest("OrderForCreationDto object is null");

        var createOrder = _serviceManager.OrderService.CreateOrder(order);
        return CreatedAtAction(nameof(GetOrderById), new { orderId = createOrder.OrderId }, createOrder);
    }

}