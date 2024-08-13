using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;
[Route("api/item")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IServiceManager _service;
    public ItemController(IServiceManager service) => _service = service;
    [HttpGet]
    public IActionResult GetItems()
    {
        try
        {
            var items = _service.ItemService.GetAllItems(trackChanges: false);
            return Ok(items);
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }

    }
    [HttpGet("product/{productId}")]
    public IActionResult GetItemsByProduct([FromRoute]Guid productId)
    {
       
        var items = _service.ItemService.GetItemsByProduct(productId);
        return Ok(items);
    }
    [HttpGet("order/{orderId}")]
    public IActionResult GetItemsByOrder([FromRoute]Guid orderId){
        var items = _service.ItemService.GetItemsByOrder(orderId);
        return Ok(items);
    }
}