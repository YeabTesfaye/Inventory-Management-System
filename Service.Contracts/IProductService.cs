using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProductService
{
   public Task<IEnumerable<ProductDto>> GetProductsAsync(Guid supplierId, bool trackChanges);
    public Task<ProductDto?> GetProductAsync(Guid productId,Guid supplierId, bool trackChanges);
    public Task<ProductDto> CreateProductAsync(ProductForCreationDto product, Guid supplierId);
    public Task DeleteProductAsync(Guid productId,Guid supplierId);
}