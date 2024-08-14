using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CustomerController(IServiceManager serviceManager)
     => _serviceManager = serviceManager;

    [HttpGet("{customerId:guid}")]
    public IActionResult GetCustomer([FromRoute] Guid customerId)
    {

        var customer = _serviceManager.CustomerService.GetCustomer(customerId, trackChanges:false);
        return Ok(customer);
    }
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CustomerForCreationDto customer)
    {
        if (customer == null)
            return BadRequest("CustomerForCreationDto object is null");
        var createCustomer = _serviceManager.CustomerService.CreateCustomer(customer);
        return CreatedAtAction(nameof(GetCustomer), new { productId = createCustomer.CustomerId, customerId = createCustomer.CustomerId }, createCustomer);
    }
}