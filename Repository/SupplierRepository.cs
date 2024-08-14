using Contracts;
using Entities.Models;

namespace Repository;

public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
{
    public SupplierRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateSupplier(Supplier supplier)
     => Create(supplier);

    public Supplier? GetSupplierById(Guid supplierId, bool trackChanges)
    => FindByCondition(s => s.SupplierId.Equals(supplierId), trackChanges).FirstOrDefault();

    public IEnumerable<Supplier> GetSuppliers(bool trackChanges)
    => [.. FindByCondition(o => true, trackChanges)];
}