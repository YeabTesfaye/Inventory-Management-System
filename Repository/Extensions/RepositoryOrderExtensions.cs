using System.Reflection;
using System.Text;
using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryOrderExtensions
{
    public static IQueryable<Order> FilterOrders(this IQueryable<Order> orders, string? orderStatus)
    {
        if (!string.IsNullOrEmpty(orderStatus))
        {
            orders = orders.Where(o => o.OrderStatus.Contains(orderStatus));
        }

        return orders;
    }

    public static IQueryable<Order> Search(this IQueryable<Order> orders, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return orders;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return orders.Where(o => o.OrderStatus.ToLower().Contains(lowerCaseTerm)); // Assuming you want to search within a description or similar field
    }
    public static IQueryable<Order> Sort(this IQueryable<Order> orders, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return orders.OrderBy(o => o.OrderDate); // Default sorting

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Order).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
            return orders.OrderBy(o => o.OrderDate); // Default sorting

        return orders.OrderBy(orderQuery);
    }
}
