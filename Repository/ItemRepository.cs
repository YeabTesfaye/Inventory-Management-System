using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Item>> GetItemsOfOrderAsync(Guid orderId, ItemParameters itemParameters, bool trackChanges)
    {
        var items = await FindByCondition(i => i.OrderId == orderId, trackChanges)
            .FilterItems(itemParameters.Name, itemParameters.Description)
            .Search(itemParameters.SearchTerm)
            .OrderBy(i => i.Name) // Or any other default ordering
            .ToListAsync();

        return PagedList<Item>
            .ToPagedList(items, itemParameters.PageNumber, itemParameters.PageSize);
    }






    public async Task<Item?> GetItemsByProductIdAsync(Guid productId)
    => await FindByCondition(i => i.ProductId == productId,
       trackChanges: false)
       .FirstOrDefaultAsync
       ();


    public void CreateItem(Item item) => Create(item);

    public void DeleteItem(Item item) => Delete(item);

    public async Task<Item?> GetItemByItemIdAsync(Guid itemId, bool trackChanges)
    {
        var item = await FindByCondition(item => item.ItemId == itemId, trackChanges: false)
        .SingleOrDefaultAsync();
        return item;
    }

}