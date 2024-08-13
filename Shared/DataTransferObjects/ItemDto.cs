namespace Shared.DataTransferObjects;

public record ItemDto(
    Guid ItemId, string? Name,
    string? Description, decimal UnitPrice,
    int QuantityInStock, Guid ProductId, Guid OrderId);
