using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers;

/// <summary>
/// Provides endpoints for managing items within orders.
/// </summary>
[Route("api/orders/{orderId}/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IServiceManager _service;

    public ItemController(IServiceManager service) => _service = service;

    /// <summary>
    /// Retrieves all items associated with a specific order.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <param name="itemParameters">Query parameters for pagination and filtering.</param>
    /// <returns>Returns a paginated list of items associated with the order.</returns>
    /// <response code="200">Returns the list of items with pagination metadata.</response>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetItemsOfOrder(Guid orderId, [FromQuery] ItemParameters itemParameters)
    {
        var pagedResult = await _service.ItemService.GetItemsOfOrderAsync(orderId, itemParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.items);
    }

    /// <summary>
    /// Retrieves a specific item by its unique identifier.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item.</param>
    /// <returns>Returns the item details if found.</returns>
    /// <response code="200">Returns the item details.</response>
    /// <response code="404">If the item is not found.</response>
    [HttpGet("{itemId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetItemsByItemId([FromRoute] Guid itemId)
    {
        var item = await _service.ItemService.GetItemByItemIdAsync(itemId, trackChanges: false);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// Retrieves items associated with a specific product within an order.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <param name="productId">The unique identifier of the product.</param>
    /// <returns>Returns a list of items associated with the product within the order.</returns>
    /// <response code="200">Returns the list of items for the specified product.</response>
    [HttpGet("product/{productId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetItemsByProductId([FromRoute] Guid orderId, [FromRoute] Guid productId)
    {
        var items = await _service.ItemService.GetItemsByProductIdAsync(orderId, productId);
        return Ok(items);
    }

    /// <summary>
    /// Creates a new item within a specific order.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <param name="item">The item data to create.</param>
    /// <returns>Returns the created item details.</returns>
    /// <response code="201">Returns the created item.</response>
    /// <response code="400">If the item data is null or invalid.</response>
    /// <response code="422">If the model state is invalid.</response>
    [HttpPost]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateItem([FromRoute] Guid orderId, [FromBody] ItemForCreationDto item)
    {
        if (item == null)
            return BadRequest("ItemForCreationDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        item.OrderId = orderId;
        var createdItem = await _service.ItemService.CreateItemAsync(orderId, item);

        return CreatedAtAction(
            nameof(GetItemsOfOrder),
            new { orderId },
            createdItem
        );
    }

    /// <summary>
    /// Updates an existing item within a specific order.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <param name="itemId">The unique identifier of the item to update.</param>
    /// <param name="item">The updated item data.</param>
    /// <returns>NoContent response if the update is successful.</returns>
    /// <response code="204">If the item is successfully updated.</response>
    /// <response code="400">If the item data is null or invalid.</response>
    /// <response code="422">If the model state is invalid.</response>
    [HttpPut("{itemId:guid}")]
    [Authorize(Roles = "Manager")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateItem([FromRoute] Guid orderId, [FromRoute] Guid itemId, [FromBody] ItemForUpdateDto item)
    {
        if (item == null)
            return BadRequest("ItemForUpdateDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.ItemService.UpdateItemAsync(orderId, itemId, item, orderTrackChanges: false, itemTrackChanges: true);
        return NoContent();
    }

    /// <summary>
    /// Deletes an item by its unique identifier.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to delete.</param>
    /// <returns>NoContent response if the deletion is successful.</returns>
    /// <response code="204">If the item is successfully deleted.</response>
    /// <response code="404">If the item is not found.</response>
    [HttpDelete("{itemId:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteItemByItemId([FromRoute] Guid itemId)
    {
        var item = await _service.ItemService.GetItemByItemIdAsync(itemId, trackChanges: false);
        if (item == null)
            return NotFound();

        await _service.ItemService.DeleteItemByItemIdAsync(itemId);
        return NoContent();
    }
}
