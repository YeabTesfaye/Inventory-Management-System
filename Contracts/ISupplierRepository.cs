using Entities.Models;

namespace Contracts;

public interface ISupplierRepository
{
    IEnumerable<Supplier> GetSuppliers(bool trackChanges);
    void CreateSupplier(Supplier supplier);
    Supplier? GetSupplierById(Guid supplierId, bool trackChanges);
    
}