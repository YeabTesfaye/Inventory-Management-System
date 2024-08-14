using Entities.Models;

namespace Contracts;

public interface ICustomerRepository
{
    Customer? GetCustomer(Guid customerId, bool trackChanges);
    void CreateCustomer(Customer customer);
}