using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class ProductForCreationDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(150, ErrorMessage = "Name cannot be longer than 150 characters.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a positive value.")]
    public int StockQuantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Reorder Level must be a positive value.")]
    public int ReorderLevel { get; set; }

    [Required(ErrorMessage = "Supplier ID is required.")]
    public Guid SupplierId { get; set; }
}