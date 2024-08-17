using Entities.Models;

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
}
