namespace ECommerce.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int ItemId { get; set; }
    public Item? Item { get; set; }

    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public decimal TotalPrice => Price * Quantity;
}
