using Entities.Models;

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
}
