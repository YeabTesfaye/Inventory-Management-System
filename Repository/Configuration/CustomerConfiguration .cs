using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(
               new Customer
               {
                   CustomerId = new Guid("bdeebf00-75c8-4c7e-8b2e-b44e63e8f5e2"),
                   FirstName = "John",
                   LastName = "Doe",
                   Email = "john.doe@example.com",
                   PhoneNumber = "123-456-7890",
                   Address = "123 Elm Street"
               },
               new Customer
               {
                   CustomerId = new Guid("a8d3c29f-59c4-43f1-92e2-68cbb1e84f7f"),
                   FirstName = "Jane",
                   LastName = "Smith",
                   Email = "jane.smith@example.com",
                   PhoneNumber = "987-654-3210",
                   Address = "456 Oak Avenue"
               }
           );
    }
}