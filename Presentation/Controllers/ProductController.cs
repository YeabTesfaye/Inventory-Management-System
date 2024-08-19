using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers;

/// <summary>
/// Provides endpoints for managing products associated with a specific supplier.
/// </summary>
[Route("api/suppliers/{supplierId}/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    /// <summary>
    /// Retrieves all products associated with a specific supplier.
    /// </summary>
    /// <param name="supplierId">The unique identifier of the supplier.</param>
    /// <param name="productParameters">Query parameters for pagination and filtering.</param>
    /// <returns>Returns a paginated list of products for the specified supplier.</returns>
    /// <response code="200">Returns the list of products with pagination metadata.</response>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProducts([FromRoute] Guid supplierId, [FromQuery] ProductParameters productParameters)
    {
        var pagedResult = await _serviceManager.ProductService.GetProductsAsync(supplierId, productParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.products);
    }

    /// <summary>
    /// Retrieves a specific product by its unique identifier within a supplier’s products.
    /// </summary>
    /// <param name="productId">The unique identifier of the product.</param>
    /// <param name="supplierId">The unique identifier of the supplier.</param>
    /// <returns>Returns the details of the specified product.</returns>
    /// <response code="200">Returns the details of the specified product.</response>
    /// <response code="404">If the product or supplier is not found.</response>
    [HttpGet("{productId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetProduct([FromRoute] Guid productId, [FromRoute] Guid supplierId)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(productId, supplierId, trackChanges: false);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    /// <summary>
    /// Creates a new product for a specific supplier.
    /// </summary>
    /// <param name="product">The product data to create.</param>
    /// <param name="supplierId">The unique identifier of the supplier.</param>
    /// <returns>Returns the created product details.</returns>
    /// <response code="201">Returns the created product.</response>
    /// <response code="400">If the product data is null or invalid.</response>
    /// <response code="422">If the model state is invalid.</response>
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
        return CreatedAtAction(nameof(GetProduct), new { productId = createProduct.ProductId, supplierId = createProduct.SupplierId }, createProduct);
    }

    /// <summary>
    /// Deletes a product by its unique identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to delete.</param>
    /// <param name="supplierId">The unique identifier of the supplier.</param>
    /// <returns>NoContent response if the deletion is successful.</returns>
    /// <response code="204">If the product is successfully deleted.</response>
    /// <response code="404">If the product or supplier is not found.</response>
    [HttpDelete("{productId:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId, [FromRoute] Guid supplierId)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(productId, supplierId, trackChanges: false);
        if (product == null)
            return NotFound();

        await _serviceManager.ProductService.DeleteProductAsync(productId, supplierId);
        return NoContent();
    }
}
