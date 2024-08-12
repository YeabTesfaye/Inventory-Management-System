using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasData(
                new Order
                {
                    OrderId = new Guid("b5a0a9ef-2e1e-4eb7-8e16-0402fa19e752"),
                    OrderDate = DateTime.Now,
                    CustomerId = new Guid("bdeebf00-75c8-4c7e-8b2e-b44e63e8f5e2"),
                    OrderStatus = "Processing",
                    TotalAmount = 59.97m
                }
            );
    }
}