using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class UserForRegistrationDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; init; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; } = string.Empty;
    public string Email { get; init; }  = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public ICollection<string> Roles { get; init; } = [];
}