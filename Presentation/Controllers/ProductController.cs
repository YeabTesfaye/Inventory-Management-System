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
    public async Task<IActionResult> GetProducts(Guid supplierId)
    {
        var products = await _serviceManager.ProductService.GetProductsAsync(supplierId, trackChanges: false);
        return Ok(products);
    }
    [HttpGet("{productId:guid}")]

    public async Task<IActionResult> GetProduct([FromRoute] Guid productId, [FromRoute] Guid supplierId)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(productId, supplierId, trackChanges: false);
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto product, [FromRoute] Guid supplierId)
    {

        if (product == null)
            return BadRequest("ProductForCreationDto object is null");
        var createProduct = await _serviceManager.ProductService.CreateProductAsync(product, supplierId);
        return CreatedAtAction(nameof(GetProduct), new { productId = createProduct.ProductId, supplierId = product.SupplierId }, createProduct);
    }

}