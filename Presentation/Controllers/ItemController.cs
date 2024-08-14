using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
    public IActionResult GetItemsByProductId([FromRoute] Guid productId)
    {

        var items = _service.ItemService.GetItemsByProductId(productId);
        return Ok(items);
    }
    
}