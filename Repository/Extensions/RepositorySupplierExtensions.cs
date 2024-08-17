using System.Reflection;
using System.Text;
using Entities.Models;

using System.Linq.Dynamic.Core;


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
    public static IQueryable<Supplier> Sort(this IQueryable<Supplier> suppliers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return suppliers.OrderBy(s => s.CompanyName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Supplier).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return suppliers.OrderBy(s => s.CompanyName);

            return suppliers.OrderBy(orderQuery);
        }
  
}
