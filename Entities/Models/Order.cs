namespace Entities.Models
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public string? OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
    }
}