using Entities.Models;

namespace Contracts;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerAsync(Guid customerId, bool trackChanges);
    void CreateCustomer(Customer customer);
}