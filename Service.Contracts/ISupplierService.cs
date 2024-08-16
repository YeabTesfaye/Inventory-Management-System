using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ISupplierService
{
    public Task<IEnumerable<SupplierDto>> GetSuppliersAsync(bool trackChanges);
    public Task<SupplierDto> CreateSupplierAsync(SupplierForCreationDto supplier);
    public Task<SupplierDto> GetSupplierByIdAsync(Guid supplierId, bool trackChanges);
    Task DeleteSupplierAsync(Guid supplierId, bool trackChanges);
    Task UpdateSupplierAsync(Guid supplierId, SupplierForUpdateDto supplier, bool trackChanges);
}