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
    public IActionResult GetSuppliers()
    {
        var suppliers = _serviceManager.SupplierService.GetSuppliers(trackChanges: false);
        return Ok(suppliers);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetSupplierById([FromRoute] Guid id)
    {
        var supplier = _serviceManager.SupplierService.GetSupplierById(id, trackChanges: false);

        return Ok(supplier);
    }

    [HttpPost]
    public IActionResult CreateSupplier([FromBody] SupplierForCreationDto supplier)
    {
        if (supplier == null)
            return BadRequest("SupplierForCreationDto object is null");

        var createSupplier = _serviceManager.SupplierService.CreateSupplier(supplier);

        // Use CreatedAtAction to reference the GetSupplierById action
        return CreatedAtAction(nameof(GetSupplierById), new { id = createSupplier.SupplierId }, createSupplier);
    }

}
