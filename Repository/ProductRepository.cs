using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(Guid supplierId, ProductParameters productParameters, bool trackChanges)
    {
        return await FindByCondition(p => p.SupplierId == supplierId, trackChanges)
            .OrderBy(p => p.Name)
            .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
            .Take(productParameters.PageSize)
            .ToListAsync();
    }


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