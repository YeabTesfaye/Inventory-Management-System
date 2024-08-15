using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class CustomerForCreationDto
{
    [Required(ErrorMessage = "First Name is required.")]
    [MaxLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required.")]
    [MaxLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(300, ErrorMessage = "Address cannot be longer than 300 characters.")]
    public string Address { get; set; } = string.Empty;
}