namespace Shared.RequestFeatures;

public class ProductParameters : RequestParameters
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string SearchTerm { get; set; } = string.Empty;
    public ProductParameters() => OrderBy = "Name";
}