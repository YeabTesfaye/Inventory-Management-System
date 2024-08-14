using Contracts;
using Entities.Exceptions;
using Entities.Models;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Product> GetProducts(Guid suppierId, bool trackChanges)
     => [.. FindByCondition(p => p.SupplierId == suppierId, trackChanges).OrderBy(p => p.Name)];

    public Product? GetProduct(Guid productId,bool trackChanges)
    {
        var product = FindByCondition(p => p.ProductId == productId,
         trackChanges).FirstOrDefault()
         ?? throw new ProductNotFoundException(productId);
        return product;
    }

    public void CreateProduct(Product product)
     => Create(product);
}