using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ItemService : IItemService
{
    private readonly IRepositoryManager _repositoryManager;
    public ItemService(IRepositoryManager repositoryManager){
        _repositoryManager = repositoryManager;
    }
}