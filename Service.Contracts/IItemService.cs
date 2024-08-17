using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IItemService
{
    Task<(IEnumerable<ItemDto> items,MetaData metaData)> GetItemsOfOrderAsync(Guid orderId, ItemParameters itemParameters,bool trackChanges);
    Task<ItemDto?> GetItemsByProductIdAsync(Guid orderId, Guid productId);
    Task<ItemDto> CreateItemAsync(Guid orderId, ItemForCreationDto item);
    Task DeleteItemAsync(Guid id, bool trackChanges);
    Task<ItemDto> GetItemByItemIdAsync(Guid id, bool trackChanges);
    Task DeleteItemByItemIdAsync(Guid id);
    Task UpdateItemAsync(Guid orderId, Guid itemId, ItemForUpdateDto item,
    bool orderTrackChanges,bool itemTrackChanges);
}