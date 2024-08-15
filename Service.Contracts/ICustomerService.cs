using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerAsync(Guid customerId,bool trackChanges);
    Task<CustomerDto> CreateCustomerAsync(CustomerForCreationDto customer);
    Task DeleteCustomerAsync(Guid id, bool trackChanges);
    
}