using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;
[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public OrderController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public IActionResult GetOrders()
    {
        var orders = _serviceManager.OrderService.GetOrders();
        return Ok(orders);
    }
    [HttpGet("{orderId}")]
    public IActionResult GetOrder([FromRoute]Guid orderId){
        
        var order = _serviceManager.OrderService.GetOrder(orderId);
        return Ok(order);
    }

}