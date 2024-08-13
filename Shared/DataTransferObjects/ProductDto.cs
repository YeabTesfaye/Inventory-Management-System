namespace Shared.DataTransferObjects;

public record ProductDto(Guid ProductId, string? Name,
string? Description, decimal Price, int StockQuantity,
int ReorderLevel, Guid SupplierId);