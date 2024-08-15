using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class OrderForCreationDto
{
    [Required(ErrorMessage = "Order Date is required.")]
    public DateTime OrderDate { get; set; }

    [Required(ErrorMessage = "Customer ID is required.")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "Order Status is required.")]
    [MaxLength(50, ErrorMessage = "Order Status cannot be longer than 50 characters.")]
    public string OrderStatus { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Total Amount must be greater than zero.")]
    public decimal TotalAmount { get; set; }
}
