using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface ISupplierService
{
    public Task<IEnumerable<SupplierDto>> GetSuppliersAsync(SupplierParameters supplierParameters, bool trackChanges);
    public Task<SupplierDto> CreateSupplierAsync(SupplierForCreationDto supplier);
    public Task<SupplierDto> GetSupplierByIdAsync(Guid supplierId, bool trackChanges);
    Task DeleteSupplierAsync(Guid supplierId, bool trackChanges);
    Task UpdateSupplierAsync(Guid supplierId, SupplierForUpdateDto supplier, bool trackChanges);
}