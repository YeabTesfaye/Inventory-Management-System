using Entities.Models;

namespace Contracts;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetItemsOfOrderAsync(Guid orderId, bool trackChanges);
    Task<Item?> GetItemsByProductIdAsync(Guid productId);
    void CreateItem(Item item);
    void DeleteItem(Item item);
    Task<Item?> GetItemByItemIdAsync(Guid itemId);
   
}