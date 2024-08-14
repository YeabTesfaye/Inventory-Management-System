using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

[Route("api/suppliers/{supplierId}/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public IActionResult GetProducts(Guid supplierId)
    {
        var products = _serviceManager.ProductService.GetProducts(supplierId, trackChanges: false);
        return Ok(products);
    }
    [HttpGet("{productId:guid}")]

    public IActionResult GetProduct([FromRoute] Guid productId)
    {
        var product = _serviceManager.ProductService.GetProduct(productId, trackChanges: false);
        return Ok(product);
    }
    [HttpPost]
    public IActionResult CreateProduct([FromBody] ProductForCreationDto product)
    {
        
        if (product == null)
            return BadRequest("ProductForCreationDto object is null");
        var createProduct = _serviceManager.ProductService.CreateProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { productId = createProduct.ProductId, supplierId = product.SupplierId }, createProduct);
    }

}