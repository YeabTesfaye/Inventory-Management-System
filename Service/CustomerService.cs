using Contracts;
using Service.Contracts;

namespace Service;

public sealed class CustomerService : ICustomerService
{
    private readonly IRepositoryManager _repositoryManager;
    public CustomerService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
}