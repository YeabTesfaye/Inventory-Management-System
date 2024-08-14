using Entities.Models;

namespace Contracts;

public interface IItemRepository
{
    IEnumerable<Item> GetItemsOfOrder(Guid orderId, bool trackChanges);
    Item? GetItemsByProductId(Guid productId);
    void CreateItem(Item item);
    
}