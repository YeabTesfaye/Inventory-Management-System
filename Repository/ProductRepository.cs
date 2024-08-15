using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(Guid suppierId, bool trackChanges)
     => await FindByCondition(p => p.SupplierId == suppierId, trackChanges).OrderBy(p => p.Name).ToListAsync();

    public async Task<Product?> GetProductAsync(Guid productId, bool trackChanges)
    {
        var product = await FindByCondition(p => p.ProductId == productId,
         trackChanges).SingleOrDefaultAsync()
         ?? throw new ProductNotFoundException(productId);
        return product;
    }

    public void CreateProduct(Product product)
     => Create(product);

    public void DeleteProduct(Product product) => Delete(product);
}