using Contracts;
using Entities.Models;

namespace Repository;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Item> GetItemsOfOrder(Guid orderId, bool trackChanges)
    => FindByCondition(item => item.OrderId == orderId, trackChanges)
        .OrderBy(item => item.Name);



    public Item? GetItemsByProductId(Guid productId)
    => FindByCondition(i => i.ProductId == productId,
       trackChanges: false)
       .FirstOrDefault();

    public void CreateItem(Item item) => Create(item);
}