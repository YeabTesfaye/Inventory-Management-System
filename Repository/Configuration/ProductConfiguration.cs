using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
                new Product
                {
                    ProductId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "Gadget",
                    Description = "A versatile gadget.",
                    Price = 99.99m,
                    StockQuantity = 50,
                    ReorderLevel = 10,
                    SupplierId = new Guid("a8d3c29f-59c4-43f1-92e2-68cbb1e84f7f")
                }
            );
    }
}