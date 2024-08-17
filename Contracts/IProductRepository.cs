using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IProductRepository
{
    public Task<PagedList<Product>> GetProductsAsync(Guid supplierId, ProductParameters productParameters, bool trackChanges);
    Task<Product?> GetProductAsync(Guid productId, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);

}