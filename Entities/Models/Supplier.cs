namespace Entities.Models;

public class Supplier
{
    public Guid SupplierId { get; set; } = Guid.NewGuid();
    public string? CompanyName { get; set; }
    public string? ContactName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
}