using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public SupplierController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetSuppliers()
    {
        var suppliers = await _serviceManager.SupplierService.GetSuppliersAsync(trackChanges: false);
        return Ok(suppliers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSupplierById([FromRoute] Guid id)
    {
        var supplier = await _serviceManager.SupplierService.GetSupplierByIdAsync(id, trackChanges: false);

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier([FromBody] SupplierForCreationDto supplier)
    {
        if (supplier == null)
            return BadRequest("SupplierForCreationDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var createSupplier = await _serviceManager.SupplierService.CreateSupplierAsync(supplier);

        // Use CreatedAtAction to reference the GetSupplierById action
        return CreatedAtAction(nameof(GetSupplierById), new { id = createSupplier.SupplierId }, createSupplier);
    }
    [HttpDelete("{supplierId:guid}")]
    public async Task<IActionResult> DeleteSupplier([FromRoute] Guid supplierId)
    {
        await _serviceManager.SupplierService.DeleteSupplierAsync(supplierId, trackChanges: false);
        return NoContent();
    }

}
