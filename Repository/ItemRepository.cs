using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Item>> GetItemsOfOrderAsync(Guid orderId, bool trackChanges)
    => await FindByCondition(item => item.OrderId == orderId, trackChanges)
        .OrderBy(item => item.Name).ToListAsync();



    public async Task<Item?> GetItemsByProductIdAsync(Guid productId)
    => await FindByCondition(i => i.ProductId == productId,
       trackChanges: false)
       .SingleOrDefaultAsync();

    public void CreateItem(Item item) => Create(item);
}