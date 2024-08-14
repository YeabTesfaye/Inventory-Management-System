using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IItemService
{
    IEnumerable<ItemDto> GetItemsOfOrder(Guid orderId, bool trackChanges);
    ItemDto? GetItemsByProductId(Guid productId);


}