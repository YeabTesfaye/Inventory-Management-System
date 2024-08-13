using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _serviceManager.ProductService.GetProducts();
        return Ok(products);
    }
    [HttpGet("{productId}")]

    public IActionResult GetProduct([FromRoute] Guid productId)
    {
        var product = _serviceManager.ProductService.GetProduct(productId);
        return Ok(product);
    }

}