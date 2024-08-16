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
    public async Task<IActionResult> GetCustomer([FromRoute] Guid customerId)
    {

        var customer = await _serviceManager.CustomerService.GetCustomerAsync(customerId, trackChanges: false);
        return Ok(customer);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreationDto customer)
    {
        if (customer == null)
            return BadRequest("CustomerForCreationDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var createCustomer = await _serviceManager.CustomerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomer), new { productId = createCustomer.CustomerId, customerId = createCustomer.CustomerId }, createCustomer);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
    {
        await _serviceManager.CustomerService.DeleteCustomerAsync(id, trackChanges: false);
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer([FromBody] CustomerForUpdateDto customer, [FromRoute] Guid id)
    {
        if (customer is null)
            return BadRequest("CustomerForUpdateDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.CustomerService.UpdateCustomerAsync(id, customer, trackChanges: true);
        return NoContent();

    }

}