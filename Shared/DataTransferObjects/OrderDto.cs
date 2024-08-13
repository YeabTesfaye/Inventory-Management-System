namespace Shared.DataTransferObjects;

public record OrderDto(Guid OrderId, DateTime OrderDate, 
Guid CustomerId, string? OrderStatus, decimal TotalAmount);