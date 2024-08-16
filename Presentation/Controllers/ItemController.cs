using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

[Route("api/orders/{orderId}/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IServiceManager _service;
    public ItemController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetItemsOfOrder(Guid orderId)
    {
        var items = await _service.ItemService.GetItemsOfOrderAsync(orderId, trackChanges: false);
        return Ok(items);
    }
    [HttpGet("{itemId:guid}")]
    public async Task<IActionResult> GetItemsByItemId([FromRoute] Guid itemId)
    {

        var items = await _service.ItemService.GetItemByItemIdAsync(itemId);
        return Ok(items);
    }

    [HttpGet("product/{productId:guid}")]
    public async Task<IActionResult> GetItemsByProductId([FromRoute] Guid orderId, [FromRoute] Guid productId)
    {
        var items = await _service.ItemService.GetItemsByProductIdAsync(orderId, productId);
        return Ok(items);
    }

    [HttpPost]
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
    [ServiceFilter(typeof(ValidationFilterAttribute))]

    public async Task<IActionResult> UpdateItem([FromBody] ItemForUpdateDto item,
   [FromRoute] Guid orderId, [FromRoute] Guid itemId)
    {
        if (item == null)
            return BadRequest("ItemForUpdateDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.ItemService.UpdateItemAsync(orderId, itemId, item, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{itemId:guid}")]
    public async Task<IActionResult> DeleteItemByItemId([FromRoute] Guid itemId)
    {

        await _service.ItemService.DeleteItemByItemIdAsync(itemId);
        return NoContent();
    }
}
