using Entities.Models;

namespace Repository.Extensions;

public static class RepositorySupplierExtensions
{
    public static IQueryable<Supplier> FilterSuppliers(this IQueryable<Supplier> suppliers, string? companyName, string? contactName)
    {
        if (!string.IsNullOrEmpty(companyName))
        {
            suppliers = suppliers.Where(s => s.CompanyName.Contains(companyName));
        }

        if (!string.IsNullOrEmpty(contactName))
        {
            suppliers = suppliers.Where(s => s.ContactName.Contains(contactName));
        }

        return suppliers;
    }

    public static IQueryable<Supplier> Search(this IQueryable<Supplier> suppliers, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return suppliers;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return suppliers.Where(s => s.CompanyName.ToLower().Contains(lowerCaseTerm) ||
                                    s.ContactName.ToLower().Contains(lowerCaseTerm));
    }
}
