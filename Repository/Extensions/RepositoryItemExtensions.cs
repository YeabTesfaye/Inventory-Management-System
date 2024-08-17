using System.Reflection;
using System.Text;
using Entities.Models;
using System.Linq.Dynamic.Core;

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
    public static IQueryable<Item> Sort(this IQueryable<Item> items, string? orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return items.OrderBy(i => i.Name); // Default sorting

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Item).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var direction = param.EndsWith(" desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        if (string.IsNullOrWhiteSpace(orderQuery))
            return items.OrderBy(i => i.Name); // Default sorting

        return items.OrderBy(orderQuery);
    }

}
