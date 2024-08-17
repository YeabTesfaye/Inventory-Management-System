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

    public async Task<PagedList<Product>> GetProductsAsync(Guid supplierId, ProductParameters productParameters, bool trackChanges)
    {
        // Apply filtering based on Name if it's not empty
        var products = await FindByCondition(p => p.SupplierId == supplierId &&
                                                  (string.IsNullOrEmpty(productParameters.Name) || p.Name.Contains(productParameters.Name)),
                                                  trackChanges)
            .OrderBy(p => p.Name)  // Ensure products are ordered by Name
            .ToListAsync();

        // Paginate the filtered results
        return PagedList<Product>
            .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
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