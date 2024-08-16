using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllSuppliersAsync(SupplierParameters supplierParameters, bool trackChanges);
    void CreateSupplier(Supplier supplier);
    Task<Supplier?> GetSupplierByIdAsync(Guid supplierId, bool trackChanges);
    void DeleteSupplier(Supplier supplier);
}
