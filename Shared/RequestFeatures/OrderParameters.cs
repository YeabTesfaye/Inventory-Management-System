namespace Shared.RequestFeatures;

public class OrderParameters : RequestParameters
{
    public string OrderStatus { get; set; } = string.Empty;
    public string SearchTerm { get; set; } = string.Empty;
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; } = decimal.MaxValue;
    public bool ValidAmountRange => MaxAmount > MinAmount;
    public OrderParameters() => OrderBy = "OrderDate";
}