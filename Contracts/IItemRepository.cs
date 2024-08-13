using Entities.Models;

namespace Contracts;

public interface IItemRepository
{
    IEnumerable<Item> GetAllItems(bool trackChanges);
    IEnumerable<Item> GetItemsByProduct(Guid productId);
    IEnumerable<Item> GetItemsByOrder(Guid orderId);
}