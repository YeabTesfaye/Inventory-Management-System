using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class ItemForUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be a positive number")]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "QuantityInStock must be a non-negative number")]
    public int QuantityInStock { get; set; }

    [Required(ErrorMessage = "ProductId is required")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "OrderId is required")]
    public Guid OrderId { get; set; }
}