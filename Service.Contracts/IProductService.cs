using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProductService
{
   public IEnumerable<ProductDto> GetProducts(Guid supplierId, bool trackChanges);
    public ProductDto? GetProduct(Guid productId,Guid supplierId, bool trackChanges);
    public ProductDto CreateProduct(ProductForCreationDto product, Guid supplierId);
}