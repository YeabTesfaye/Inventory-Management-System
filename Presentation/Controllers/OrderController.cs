using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

}