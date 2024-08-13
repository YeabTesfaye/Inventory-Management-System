namespace Shared.DataTransferObjects;

public record SupplierDto(Guid SupplierId, string? CompanyName, string? ContactName, string? PhoneNumber, string? Email, string? Address);