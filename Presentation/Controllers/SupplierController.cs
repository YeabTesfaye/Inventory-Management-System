using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;

[Route("api/supplier")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public SupplierController(IServiceManager serviceManager){
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public IActionResult GetSuppliers(){
        var suppliers = _serviceManager.SupplierService.GetSuppliers(trackChanges:false);
        return Ok(suppliers);
    }
}