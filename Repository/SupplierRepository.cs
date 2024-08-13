using Contracts;
using Entities.Models;

namespace Repository;

public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
{
    public SupplierRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}