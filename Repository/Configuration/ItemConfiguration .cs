using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasData(
                new Item
                {
                    ItemId = new Guid("d3f7e058-0ae6-4c6e-9f42-13d0d1b6a6a3"),
                    Name = "Widget",
                    Description = "A useful widget.",
                    UnitPrice = 19.99m,
                    QuantityInStock = 100,
                    ProductId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    OrderId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a")
                }
            );
    }
}