using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class UserForAuthenticationDto
{
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; init; } = string.Empty;
    [Required(ErrorMessage = "Password name is required")]
    public string Password { get; init; } = string.Empty;
}