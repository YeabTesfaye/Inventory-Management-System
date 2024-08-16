using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class CustomerForUpdateDto
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "PhoneNumber is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}