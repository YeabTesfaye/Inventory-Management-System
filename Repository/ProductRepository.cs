using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Product>> GetProductsAsync(Guid supplierId, ProductParameters productParameters, bool trackChanges)
    {
        Console.WriteLine("Hello");
        var products = await FindByCondition(p => p.SupplierId == supplierId, trackChanges)
            .FilterProducts(productParameters.Name, productParameters.Description)
            .Search(productParameters.SearchTerm)
            .Sort(productParameters.OrderBy)
            .ToListAsync();

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