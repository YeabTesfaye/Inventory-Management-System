namespace Entities.Models;

public class Item
{
    public Guid ItemId { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
    public Guid ProductId { get; set; } // Foreign key to Product
    public Guid OrderId { get; set; } // Foreign key to Order
}