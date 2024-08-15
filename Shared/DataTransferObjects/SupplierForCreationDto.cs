using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class SupplierForCreationDto
{
    [Required(ErrorMessage = "Company Name is required.")]
    [MaxLength(200, ErrorMessage = "Company Name cannot be longer than 200 characters.")]
    public string CompanyName { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Contact Name cannot be longer than 100 characters.")]
    public string? ContactName { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string? PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string? Email { get; set; }

    [MaxLength(300, ErrorMessage = "Address cannot be longer than 300 characters.")]
    public string? Address { get; set; }
}