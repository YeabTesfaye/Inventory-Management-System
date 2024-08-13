using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CustomerController(IServiceManager serviceManager)
     => _serviceManager = serviceManager;

    [HttpGet("{customerId}")]
    public IActionResult GetCustomer([FromRoute] Guid customerId)
    {
        Console.WriteLine(customerId);
        var customer = _serviceManager.CustomerService.GetCustomer(customerId);
        return Ok(customer);

    }
}