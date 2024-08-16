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

    public async Task<CustomerDto> CreateCustomerAsync(CustomerForCreationDto customer)
    {
        var createEntity = _mapper.Map<Customer>(customer);
        _repositoryManager.Customer.CreateCustomer(createEntity);
        await _repositoryManager.SaveAsync();
        var customerToReturn = _mapper.Map<CustomerDto>(createEntity);
        return customerToReturn;
    }

    public async Task DeleteCustomerAsync(Guid id, bool trackChanges)
    {
        var customer = await GetCustomerAndCheckIfItExists(id, trackChanges: false);
        _repositoryManager.Customer.DeleteCustomer(customer);
        await _repositoryManager.SaveAsync();
    }

    public async Task<CustomerDto?> GetCustomerAsync(Guid customerId, bool trackChanges)
    {
        var customer = await _repositoryManager.Customer.GetCustomerAsync(customerId, trackChanges)
         ?? throw new CustomerNotFoundException(customerId);
        var customerDto = _mapper.Map<CustomerDto>(customer);
        return customerDto;
    }

    public async Task UpdateCustomerAsync(Guid id, CustomerForUpdateDto customer, bool trackChanges)
    {
        // Fetch the customer from the repository with tracking as specified
        var customerEntity = await _repositoryManager.Customer.GetCustomerAsync(id, trackChanges)
                             ?? throw new CustomerNotFoundException(id);

        // Map the DTO to the entity
        _mapper.Map(customer, customerEntity);

        // Save the changes to the database
        await _repositoryManager.SaveAsync();
    }


    private async Task<Customer> GetCustomerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var customer = await _repositoryManager.Customer.GetCustomerAsync(id, trackChanges)
         ?? throw new CustomerNotFoundException(id);
        return customer;
    }

}