namespace ECommerce.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending"; // Pending, Complete

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public decimal TotalPrice => OrderItems.Sum(oi => oi.TotalPrice);
}
