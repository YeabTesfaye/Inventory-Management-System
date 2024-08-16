using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetItemsOfOrderAsync(Guid orderId, ItemParameters itemParameters,bool trackChanges);
    Task<Item?> GetItemsByProductIdAsync(Guid productId);
    void CreateItem(Item item);
    void DeleteItem(Item item);
    Task<Item?> GetItemByItemIdAsync(Guid itemId);
   
}