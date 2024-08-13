using Contracts;
using Service.Contracts;

namespace Service;

public sealed class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    public OrderService(IRepositoryManager repositoryManager){
        _repositoryManager = repositoryManager;
    }
}