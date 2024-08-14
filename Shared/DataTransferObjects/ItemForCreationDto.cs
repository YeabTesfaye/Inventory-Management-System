namespace Shared.DataTransferObjects;

public class ItemForCreationDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
}