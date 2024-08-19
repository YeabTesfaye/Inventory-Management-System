using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers;

/// <summary>
/// Provides endpoints for managing orders associated with a specific customer.
/// </summary>
[Route("api/customers/{customerId}/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public OrderController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    /// <summary>
    /// Retrieves all orders associated with a specific customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="orderParameters">Query parameters for pagination and filtering.</param>
    /// <returns>Returns a paginated list of orders for the specified customer.</returns>
    /// <response code="200">Returns the list of orders with pagination metadata.</response>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOrdersOfCustomer([FromRoute] Guid customerId, [FromQuery] OrderParameters orderParameters)
    {
        var pagedResult = await _serviceManager.OrderService.GetOrdersOfCustomerAsync(customerId, orderParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.orders);
    }

    /// <summary>
    /// Retrieves a specific order by its unique identifier within a customer’s orders.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>Returns the details of the specified order.</returns>
    /// <response code="200">Returns the details of the specified order.</response>
    /// <response code="404">If the order or customer is not found.</response>
    [HttpGet("{orderId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId, [FromRoute] Guid customerId)
    {
        var order = await _serviceManager.OrderService.GetOrderByIdAsync(orderId, customerId, trackChanges: false);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// Creates a new order for a specific customer.
    /// </summary>
    /// <param name="order">The order data to create.</param>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>Returns the created order details.</returns>
    /// <response code="201">Returns the created order.</response>
    /// <response code="400">If the order data is null or invalid.</response>
    /// <response code="422">If the model state is invalid.</response>
    [HttpPost]
    [Authorize]
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

    /// <summary>
    /// Updates an existing order for a specific customer.
    /// </summary>
    /// <param name="order">The updated order data.</param>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="itemId">The unique identifier of the order to update.</param>
    /// <returns>NoContent response if the update is successful.</returns>
    /// <response code="204">If the order is successfully updated.</response>
    /// <response code="400">If the order data is null or invalid.</response>
    /// <response code="422">If the model state is invalid.</response>
    [HttpPut("{itemId:guid}")]
    [Authorize(Roles = "Manager")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderForUpdateDto order, [FromRoute] Guid customerId, [FromRoute] Guid itemId)
    {
        if (order == null)
            return BadRequest("OrderForUpdateDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.OrderService.UpdateOrder(customerId, itemId, order, trackChanges: true);
        return NoContent();
    }

    /// <summary>
    /// Deletes an order by its unique identifier.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order to delete.</param>
    /// <returns>NoContent response if the deletion is successful.</returns>
    /// <response code="204">If the order is successfully deleted.</response>
    /// <response code="404">If the order is not found.</response>
    [HttpDelete("{orderId:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteOrderByOrderId([FromRoute] Guid orderId)
    {
        var order = await _serviceManager.OrderService.GetOrderByIdAsync(orderId, Guid.Empty, trackChanges: false);
        if (order == null)
            return NotFound();

        await _serviceManager.OrderService.DeleteOrderAsync(orderId);
        return NoContent();
    }
}
