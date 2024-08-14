namespace Shared.DataTransferObjects;

public class ProductForCreationDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; }
    public Guid SupplierId { get; set; }
}