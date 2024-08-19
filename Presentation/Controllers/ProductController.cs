using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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
    [Authorize]
    public async Task<IActionResult> GetProducts(Guid supplierId, [FromQuery] ProductParameters productParameters)
    {
        var pagedResult = await _serviceManager.ProductService.GetProductsAsync(supplierId, productParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.products);
    }
    [HttpGet("{productId:guid}")]
    [Authorize]

    public async Task<IActionResult> GetProduct([FromRoute] Guid productId, [FromRoute] Guid supplierId)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(productId, supplierId, trackChanges: false);
        return Ok(product);
    }
    [HttpPost]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]

    public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto product, [FromRoute] Guid supplierId)
    {

        if (product == null)
            return BadRequest("ProductForCreationDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var createProduct = await _serviceManager.ProductService.CreateProductAsync(product, supplierId);
        return CreatedAtAction(nameof(GetProduct), new { productId = createProduct.ProductId, supplierId = product.SupplierId }, createProduct);
    }
    [HttpDelete("{productId:guid}")]
    [Authorize]

    public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId, [FromRoute] Guid supplierId)
    {
        await _serviceManager.ProductService.DeleteProductAsync(productId, supplierId);
        return NoContent();
    }

}