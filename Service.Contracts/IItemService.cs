using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetItemsOfOrderAsync(Guid orderId, bool trackChanges);
    Task<ItemDto?> GetItemsByProductIdAsync(Guid orderId,Guid productId);
    Task<ItemDto> CreateItemAsync(Guid orderId, ItemForCreationDto item);

}