using Entities.Models;

namespace Contracts;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts(Guid supplierId, bool trackChanges);
Product? GetProduct(Guid productId,bool trackChanges);
}