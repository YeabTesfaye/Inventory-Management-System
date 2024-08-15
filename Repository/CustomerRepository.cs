using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateCustomer(Customer customer)
     => Create(customer);

    public void DeleteCustomer(Customer customer) => Delete(customer);

    public async Task<Customer?> GetCustomerAsync(Guid customerId, bool trackChanges)
    => await FindByCondition(c => c.CustomerId.Equals(customerId),
        trackChanges)
        .SingleOrDefaultAsync();



}

