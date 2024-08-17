using System.Reflection;
using System.Text;
using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryProductExtensions
{
    public static IQueryable<Product> FilterProducts(this IQueryable<Product> products, string? name, string? description)
    {
        if (!string.IsNullOrEmpty(name))
        {
            products = products.Where(p => p.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(description))
        {
            products = products.Where(p => p.Description.Contains(description));
        }

        return products;
    }

    public static IQueryable<Product> Search(this IQueryable<Product> products, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return products;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return products.Where(p => p.Name.ToLower().Contains(lowerCaseTerm) ||
                                    p.Description.ToLower().Contains(lowerCaseTerm));
    }
    public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return products.OrderBy(p => p.Name);

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        if (string.IsNullOrWhiteSpace(orderQuery))
            return products.OrderBy(p => p.Name);

        return products.OrderBy(orderQuery);
    }
}
