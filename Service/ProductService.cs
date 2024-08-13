using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
   public ProductService(IRepositoryManager repositoryManager, IMapper mapper){
        _repositoryManager = repositoryManager;
        _mapper = mapper;	
    }

    public ProductDto GetProduct(Guid productId)
    {
        var product = _repositoryManager.Product.GetProduct(productId);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }

    public IEnumerable<ProductDto> GetProducts()
    {
        var products = _repositoryManager.Product.GetAllProducts(trackChanges:false);
        var producsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return producsDto;
    }
}