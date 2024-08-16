using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class SupplierForUpdateDto
{
    [Required(ErrorMessage = "CompanyName is required")]
    public string CompanyName { get; set; } = string.Empty;

    public string? ContactName { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    public string? Address { get; set; }
}