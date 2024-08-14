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
    public IActionResult GetItemsOfOrder(Guid orderId)
    {
        var items = _service.ItemService.GetItemsOfOrder(orderId, trackChanges: false);
        return Ok(items);
    }

    [HttpGet("product/{productId:guid}")]
    public IActionResult GetItemsByProductId([FromRoute] Guid orderId, [FromRoute] Guid productId)
    {
        var items = _service.ItemService.GetItemsByProductId(orderId, productId);
        return Ok(items);
    }

    [HttpPost]
    public IActionResult CreateItem([FromRoute] Guid orderId, [FromBody] ItemForCreationDto item)
    {
        if (item == null)
            return BadRequest("ItemForCreationDto object is null");
        item.OrderId = orderId;
        var createItem = _service.ItemService.CreateItem(orderId, item);

        return CreatedAtAction(
            nameof(GetItemsOfOrder),
            new { orderId },
            createItem
        );
    }
}
