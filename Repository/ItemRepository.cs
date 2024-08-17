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

    public async Task<PagedList<Item>> GetItemsOfOrderAsync(Guid orderId, ItemParameters itemParameters, bool trackChanges)
    {
        // Apply filtering based on Name and Description if they are not empty
        var query = FindByCondition(item => item.OrderId == orderId &&
                                            (string.IsNullOrEmpty(itemParameters.Name) || item.Name.Contains(itemParameters.Name)) &&
                                            (string.IsNullOrEmpty(itemParameters.Description) || item.Description.Contains(itemParameters.Description)),
                                            trackChanges);

        // Apply ordering
        query = query.OrderBy(item => item.Name);

        // Apply pagination
        var items = await query
            .Skip((itemParameters.PageNumber - 1) * itemParameters.PageSize)
            .Take(itemParameters.PageSize)
            .ToListAsync();

        // Return paginated result
        return PagedList<Item>.ToPagedList(items, itemParameters.PageNumber, itemParameters.PageSize);
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