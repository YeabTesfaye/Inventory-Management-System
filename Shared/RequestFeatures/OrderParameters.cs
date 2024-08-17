namespace Shared.RequestFeatures;

public class OrderParameters : RequestParameters
{
    public string OrderStatus { get; set; } = string.Empty;
    public string SearchTerm { get; set; } = string.Empty;

}