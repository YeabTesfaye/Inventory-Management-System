using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;


public class ProductForUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "StockQuantity must be a non-negative number")]
    public int StockQuantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "ReorderLevel must be a non-negative number")]
    public int ReorderLevel { get; set; }

    [Required(ErrorMessage = "SupplierId is required")]
    public Guid SupplierId { get; set; }
}