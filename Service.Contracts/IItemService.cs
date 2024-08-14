using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IItemService
{
    IEnumerable<ItemDto> GetItemsOfOrder(Guid orderId, bool trackChanges);
    ItemDto? GetItemsByProductId(Guid orderId,Guid productId);
    ItemDto CreateItem(Guid orderId, ItemForCreationDto item);

}