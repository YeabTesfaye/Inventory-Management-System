using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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
    public async Task<IActionResult> GetOrdersOfCustomer([FromRoute] Guid customerId, [FromQuery] OrderParameters orderParameters)
    {
        var pagedResult = await _serviceManager.OrderService.GetOrdersOfCustomerAsync(customerId, orderParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.orders);
    }
    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId, [FromRoute] Guid customerId)
    {
        var order = await _serviceManager.OrderService.GetOrderByIdAsync(orderId, customerId, trackChanges: false);
        return Ok(order);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]

    public async Task<IActionResult> CreateOrder([FromBody] OrderForCreationDto order, [FromRoute] Guid customerId)
    {
        if (order == null)
            return BadRequest("OrderForCreationDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var createOrder = await _serviceManager.OrderService.CreateOrderAsync(order, customerId);
        return CreatedAtAction(nameof(GetOrderById), new { orderId = createOrder.OrderId, customerId }, createOrder);
    }

    [HttpPut("{itemId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]

    public async Task<IActionResult> UpdateOrder([FromBody] OrderForUpdateDto order, [FromRoute] Guid customerId,
    [FromRoute] Guid itemId)
    {
        if (order is null)
            return BadRequest("OrderForUpdateDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        await _serviceManager.OrderService.UpdateOrder(customerId, itemId, order, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> DeleteOrderByOrderId([FromRoute] Guid orderId)
    {
        await _serviceManager.OrderService.DeleteOrderAsync(orderId);
        return NoContent();
    }


}