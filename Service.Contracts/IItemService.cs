using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IItemService
{
    IEnumerable<ItemDto> GetAllItems(bool trackChanges);
    IEnumerable<ItemDto> GetItemsByProduct(Guid productId);
    IEnumerable<ItemDto> GetItemsByOrder(Guid orderId);

}