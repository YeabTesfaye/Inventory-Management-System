using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public ProductDto CreateProduct(ProductForCreationDto product)
    {
        var productEntity = _mapper.Map<Product>(product);
        _repositoryManager.Product.CreateProduct(productEntity);
        _repositoryManager.Save();
        var productToReturn = _mapper.Map<ProductDto>(productEntity);
        return productToReturn;

    }

    public ProductDto? GetProduct(Guid productId, bool trackChanges)
    {
        var product = _repositoryManager.Product.GetProduct(productId,trackChanges)
         ?? throw new ProductNotFoundException(productId);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }


    public IEnumerable<ProductDto> GetProducts(Guid supplierId, bool trackChanges)
    {
        var products = _repositoryManager.Product.GetProducts(supplierId, trackChanges);
        var producsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return producsDto;
    }
}