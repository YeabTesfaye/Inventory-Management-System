using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class CustomerService : ICustomerService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public CustomerService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public CustomerDto? GetCustomer(Guid customerId)
    {
        var customer = _repositoryManager.Customer.GetCustomer(customerId)
         ?? throw new CustomerNotFoundException(customerId);
        var customerDto = _mapper.Map<CustomerDto>(customer);
        return customerDto;
    }

}