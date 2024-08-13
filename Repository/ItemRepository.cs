using Contracts;
using Entities.Models;

namespace Repository;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Item> GetAllItems(bool trackChanges)
    => [.. FindAll(trackChanges).OrderBy(i => i.Name)];

    public IEnumerable<Item> GetItemsByProduct(Guid productId)
    => [.. FindByCondition(i => i.ProductId == productId,
       trackChanges: false)
       .OrderBy(i => i.Name)];

    public IEnumerable<Item> GetItemsByOrder(Guid orderId)
    => [.. FindByCondition( i => i.OrderId == orderId,
    trackChanges:false)
    .OrderBy(i => i.Name)];

}