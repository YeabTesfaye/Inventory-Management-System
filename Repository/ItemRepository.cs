using Contracts;
using Entities.Models;

namespace Repository;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}