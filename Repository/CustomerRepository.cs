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

    public Customer? GetCustomer(Guid customerId)
    {
        var customer = FindByCondition(c => c.CustomerId.Equals(customerId),
        trackChanges: false)
        .SingleOrDefault();
       
        return customer;
    }

}

