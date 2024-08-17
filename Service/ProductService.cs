using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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

    public async Task<ProductDto> CreateProductAsync(ProductForCreationDto product, Guid supplierId)
    {
        await CheckIfSupplierExists(supplierId, trackChanges: false);
        var productEntity = _mapper.Map<Product>(product);
        _repositoryManager.Product.CreateProduct(productEntity);
        await _repositoryManager.SaveAsync();
        var productToReturn = _mapper.Map<ProductDto>(productEntity);
        return productToReturn;

    }

    public async Task DeleteProductAsync(Guid productId, Guid supplierId)
    {

        var product = await CheckIfProductExistsAndReturn(productId, trackChanges: false);
        _repositoryManager.Product.DeleteProduct(product);
        await _repositoryManager.SaveAsync();
    }

    public async Task<ProductDto?> GetProductAsync(Guid productId, Guid supplierId, bool trackChanges)
    {
        await CheckIfSupplierExists(supplierId, trackChanges: false);
        var product = await _repositoryManager.Product.GetProductAsync(productId, trackChanges)
         ?? throw new ProductNotFoundException(productId);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }


    public async Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetProductsAsync(Guid supplierId, ProductParameters productParameters, bool trackChanges)
    {
        await CheckIfSupplierExists(supplierId, trackChanges: false);
        var productsWithMetaData = await _repositoryManager.Product.GetProductsAsync(supplierId, productParameters, trackChanges);
        var producsDto = _mapper.Map<IEnumerable<ProductDto>>(productsWithMetaData);
        return (products: producsDto, metaData: productsWithMetaData.MetaData);
    }

    private async Task CheckIfSupplierExists(Guid supplierId, bool trackChanges)
    {
        _ = await _repositoryManager.Supplier.GetSupplierByIdAsync(supplierId, trackChanges)
        ?? throw new SupplierNotFoundException(supplierId);
    }
    private async Task<Product> CheckIfProductExistsAndReturn(Guid productId, bool trackChanges)
    {
        var product = await _repositoryManager.Product.GetProductAsync(productId, trackChanges)
          ?? throw new ProductNotFoundException(productId);
        return product;
    }
}