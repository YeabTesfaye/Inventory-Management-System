using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class ItemForCreationDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than zero.")]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be a positive value.")]
    public int QuantityInStock { get; set; }

    [Required(ErrorMessage = "Product ID is required.")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Order ID is required.")]
    public Guid OrderId { get; set; }
}