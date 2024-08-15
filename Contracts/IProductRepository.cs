using Entities.Models;

namespace Contracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(Guid supplierId, bool trackChanges);
    Task<Product?> GetProductAsync(Guid productId, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);

}