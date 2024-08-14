using Contracts;
using Entities.Models;

namespace Repository;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateCustomer(Customer customer)
     => Create(customer);

    public Customer? GetCustomer(Guid customerId, bool trackChanges)
    {
        var customer = FindByCondition(c => c.CustomerId.Equals(customerId),
        trackChanges)
        .SingleOrDefault();
       
        return customer;
    }

}

