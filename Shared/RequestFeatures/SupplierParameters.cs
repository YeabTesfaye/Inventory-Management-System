namespace Shared.RequestFeatures;

public class SupplierParameters : RequestParameters
{
    public SupplierParameters() => OrderBy = "companyName";
    public string? CompanyName { get; set; }
    public string? ContactName { get; set; }
    public string? SearchTerm { get; set; }

}