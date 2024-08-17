using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IProductService
{
    public Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetProductsAsync(Guid supplierId, ProductParameters productParameters, bool trackChanges);
    public Task<ProductDto?> GetProductAsync(Guid productId, Guid supplierId, bool trackChanges);
    public Task<ProductDto> CreateProductAsync(ProductForCreationDto product, Guid supplierId);
    public Task DeleteProductAsync(Guid productId, Guid supplierId);
}