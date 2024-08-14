namespace Shared.DataTransferObjects;

public class SupplierForCreationDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
}