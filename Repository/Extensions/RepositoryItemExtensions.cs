using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryItemExtensions
{
    public static IQueryable<Item> FilterItems(this IQueryable<Item> items, string? name, string? description)
    {
        if (!string.IsNullOrEmpty(name))
        {
            items = items.Where(i => i.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(description))
        {
            items = items.Where(i => i.Description.Contains(description));
        }

        return items;
    }

    public static IQueryable<Item> Search(this IQueryable<Item> items, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return items;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return items.Where(i => i.Name.ToLower().Contains(lowerCaseTerm) ||
                                i.Description.ToLower().Contains(lowerCaseTerm));
    }
}
