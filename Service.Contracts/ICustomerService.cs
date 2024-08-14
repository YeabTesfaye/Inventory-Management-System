using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICustomerService
{
    CustomerDto? GetCustomer(Guid customerId,bool trackChanges);
    CustomerDto CreateCustomer(CustomerForCreationDto customer);
}