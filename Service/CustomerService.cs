using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
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

    public CustomerDto CreateCustomer(CustomerForCreationDto customer)
    {
        var createEntity = _mapper.Map<Customer>(customer);
        _repositoryManager.Customer.CreateCustomer(createEntity);
        _repositoryManager.Save();
        var customerToReturn = _mapper.Map<CustomerDto>(createEntity);
        return customerToReturn;
    }
     

    public CustomerDto? GetCustomer(Guid customerId, bool trackChanges)
    {
        var customer = _repositoryManager.Customer.GetCustomer(customerId,trackChanges)
         ?? throw new CustomerNotFoundException(customerId);
        var customerDto = _mapper.Map<CustomerDto>(customer);
        return customerDto;
    }

}