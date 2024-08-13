using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
   public ProductService(IRepositoryManager repositoryManager){
        _repositoryManager = repositoryManager;
    }
}