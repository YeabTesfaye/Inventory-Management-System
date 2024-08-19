using System.ComponentModel;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers;

[Route("api/orders/{orderId}/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IServiceManager _service;
    public ItemController(IServiceManager service) => _service = service;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetItemsOfOrder(Guid orderId, [FromQuery] ItemParameters itemParameters)
    {
        var pagedResult = await _service.ItemService.GetItemsOfOrderAsync(orderId, itemParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.items);
    }
    [HttpGet("{itemId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetItemsByItemId([FromRoute] Guid itemId)
    {

        var items = await _service.ItemService.GetItemByItemIdAsync(itemId, trackChanges: false);
        return Ok(items);
    }

    [HttpGet("product/{productId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetItemsByProductId([FromRoute] Guid orderId, [FromRoute] Guid productId)
    {
        var items = await _service.ItemService.GetItemsByProductIdAsync(orderId, productId);
        return Ok(items);
    }

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
        var createItem = await _service.ItemService.CreateItemAsync(orderId, item);

        return CreatedAtAction(
            nameof(GetItemsOfOrder),
            new { orderId },
            createItem
        );
    }
    [HttpPut("{itemId:guid}")]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateItem([FromRoute] Guid orderId,
    [FromRoute] Guid itemId, [FromBody] ItemForUpdateDto item
   )
    {
        if (item is null)
            return BadRequest("ItemForUpdateDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.ItemService.UpdateItemAsync(orderId, itemId, item,
        orderTrackChanges: false, itemTrackChanges: true);
        return NoContent();
    }

    [HttpDelete("{itemId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteItemByItemId([FromRoute] Guid itemId)
    {

        await _service.ItemService.DeleteItemByItemIdAsync(itemId);
        return NoContent();
    }
}
