namespace Shared.DataTransferObjects;

public class OrderForCreationDto
{
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}