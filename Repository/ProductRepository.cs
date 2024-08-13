using Contracts;
using Entities.Models;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Product> GetAllProducts(bool trackChanges)
     => [.. FindByCondition(o => true, trackChanges).OrderBy(p => p.Name)];

    public Product GetProduct(Guid productId)
     => FindByCondition(p => p.ProductId == productId, trackChanges: false).FirstOrDefault();
}