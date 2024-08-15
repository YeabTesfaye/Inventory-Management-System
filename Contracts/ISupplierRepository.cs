using Entities.Models;

namespace Contracts;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllSuppliersAsync(bool trackChanges);
    void CreateSupplier(Supplier supplier);
    Task<Supplier?> GetSupplierByIdAsync(Guid supplierId, bool trackChanges);
    void DeleteSupplier(Supplier supplier);
}
