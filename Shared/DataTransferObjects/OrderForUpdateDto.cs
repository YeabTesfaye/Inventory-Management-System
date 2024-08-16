using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class OrderForUpdateDto
{
    [Required(ErrorMessage = "OrderDate is required")]
    public DateTime OrderDate { get; set; }

    [Required(ErrorMessage = "CustomerId is required")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "OrderStatus is required")]
    public string OrderStatus { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be a positive number")]
    public decimal TotalAmount { get; set; }
}
