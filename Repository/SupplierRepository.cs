using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
{
    public SupplierRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateSupplier(Supplier supplier)
        => Create(supplier);

    public void DeleteSupplier(Supplier supplier)
        => Delete(supplier);

    public async Task<Supplier?> GetSupplierByIdAsync(Guid supplierId, bool trackChanges)
        => await FindByCondition(s => s.SupplierId.Equals(supplierId), trackChanges).SingleOrDefaultAsync();

    public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync(bool trackChanges)
        => await FindByCondition(o => true, trackChanges).ToListAsync();
}
