namespace Entities.Models;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public string? Role { get; set; }
    public string? Email { get; set; }
}