using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing suppliers.
    /// </summary>
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SupplierController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Retrieves all suppliers with optional filtering and pagination.
        /// </summary>
        /// <param name="supplierParameters">Query parameters for pagination and filtering.</param>
        /// <returns>Returns a paginated list of suppliers.</returns>
        /// <response code="200">Returns the list of suppliers with pagination metadata.</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSuppliers([FromQuery] SupplierParameters supplierParameters)
        {
            var pagedResult = await _serviceManager.SupplierService.GetSuppliersAsync(supplierParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.suppliers);
        }

        /// <summary>
        /// Retrieves a specific supplier by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the supplier.</param>
        /// <returns>Returns the details of the specified supplier.</returns>
        /// <response code="200">Returns the details of the specified supplier.</response>
        /// <response code="404">If the supplier with the specified ID is not found.</response>
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetSupplierById([FromRoute] Guid id)
        {
            var supplier = await _serviceManager.SupplierService.GetSupplierByIdAsync(id, trackChanges: false);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        /// <summary>
        /// Creates a new supplier.
        /// </summary>
        /// <param name="supplier">The supplier data to create.</param>
        /// <returns>Returns the created supplier details.</returns>
        /// <response code="201">Returns the created supplier.</response>
        /// <response code="400">If the supplier data is null.</response>
        /// <response code="422">If the model state is invalid.</response>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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

        /// <summary>
        /// Deletes a supplier by its unique identifier.
        /// </summary>
        /// <param name="supplierId">The unique identifier of the supplier to delete.</param>
        /// <returns>NoContent response if the deletion is successful.</returns>
        /// <response code="204">If the supplier is successfully deleted.</response>
        /// <response code="404">If the supplier with the specified ID is not found.</response>
        [HttpDelete("{supplierId:guid}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid supplierId)
        {
            var supplier = await _serviceManager.SupplierService.GetSupplierByIdAsync(supplierId, trackChanges: false);
            if (supplier == null)
                return NotFound();

            await _serviceManager.SupplierService.DeleteSupplierAsync(supplierId, trackChanges: false);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing supplier by its unique identifier.
        /// </summary>
        /// <param name="supplier">The updated supplier data.</param>
        /// <param name="supplierId">The unique identifier of the supplier to update.</param>
        /// <returns>NoContent response if the update is successful.</returns>
        /// <response code="204">If the supplier is successfully updated.</response>
        /// <response code="400">If the supplier data is null.</response>
        /// <response code="422">If the model state is invalid.</response>
        [HttpPut("{supplierId:guid}")]
        [Authorize(Roles = "Manager")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierForUpdateDto supplier, [FromRoute] Guid supplierId)
        {
            if (supplier == null)
                return BadRequest("SupplierForUpdateDto object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _serviceManager.SupplierService.UpdateSupplierAsync(supplierId, supplier, trackChanges: true);
            return NoContent();
        }
    }
}
