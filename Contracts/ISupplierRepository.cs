using Entities.Models;

namespace Contracts;

public interface ISupplierRepository
{
    IEnumerable<Supplier> GetSuppliers(bool trackChanges);
}