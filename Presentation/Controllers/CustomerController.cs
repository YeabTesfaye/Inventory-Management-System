using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;
/// <summary>
/// Provides endpoints for managing customer data.
/// </summary>
[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CustomerController(IServiceManager serviceManager)
     => _serviceManager = serviceManager;
    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>Returns the customer details if found, otherwise a NotFound response.</returns>
    /// <response code="200">Returns the customer details.</response>
    /// <response code="404">If the customer is not found.</response>
    [HttpGet("{customerId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetCustomer([FromRoute] Guid customerId)
    {

        var customer = await _serviceManager.CustomerService.GetCustomerAsync(customerId, trackChanges: false);
        return Ok(customer);
    }
    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customer">The customer data to create.</param>
    /// <returns>Returns the created customer details.</returns>
    /// <response code="201">Returns the created customer.</response>
    /// <response code="400">If the customer data is null or invalid.</response>
    /// <response code="422">If the model state is invalid.</response>
    [HttpPost]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]

    public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreationDto customer)
    {
        if (customer == null)
            return BadRequest("CustomerForCreationDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var createCustomer = await _serviceManager.CustomerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomer), new { productId = createCustomer.CustomerId, customerId = createCustomer.CustomerId }, createCustomer);
    }
    /// <summary>
    /// Deletes a customer by their unique identifier.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer to delete.</param>
    /// <returns>NoContent response if the deletion is successful.</returns>
    /// <response code="204">If the customer is successfully deleted.</response>
    /// <response code="404">If the customer is not found.</response>
    [HttpDelete("{customerId:guid}")]
    [Authorize(Roles = "Administrator")]

    public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
    {
        await _serviceManager.CustomerService.DeleteCustomerAsync(id, trackChanges: false);
        return NoContent();
    }
    /// <summary>
    /// Updates an existing customer by their unique identifier.
    /// </summary>
    /// <param name="customer">The customer data to update.</param>
    /// <param name="customerId">The unique identifier of the customer to update.</param>
    /// <returns>NoContent response if the update is successful.</returns>
    /// <response code="204">If the customer is successfully updated.</response>
    /// <response code="400">If the customer data is null or invalid.</response>
    /// <response code="422">If the model state is invalid.</response>
    [HttpPut("{customerId:guid}")]
    [Authorize(Roles = "Manager")]

    [ServiceFilter(typeof(ValidationFilterAttribute))]

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