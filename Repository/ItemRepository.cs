using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Item>> GetItemsOfOrderAsync(Guid orderId, ItemParameters itemParameters, bool trackChanges)
    => await FindByCondition(item => item.OrderId == orderId, trackChanges)
        .OrderBy(item => item.Name)
        .Skip((itemParameters.PageNumber - 1) * itemParameters.PageSize)
        .Take(itemParameters.PageSize).
        ToListAsync();



    public async Task<Item?> GetItemsByProductIdAsync(Guid productId)
    => await FindByCondition(i => i.ProductId == productId,
       trackChanges: false)
       .FirstOrDefaultAsync
       ();


    public void CreateItem(Item item) => Create(item);

    public void DeleteItem(Item item) => Delete(item);

    public async Task<Item?> GetItemByItemIdAsync(Guid itemId)
    {
        var item = await FindByCondition(item => item.ItemId == itemId, trackChanges: false)
        .SingleOrDefaultAsync();
        return item;
    }



}