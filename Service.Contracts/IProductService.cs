using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProductService
{
   public IEnumerable<ProductDto> GetProducts();
    public ProductDto GetProduct(Guid productId);
}