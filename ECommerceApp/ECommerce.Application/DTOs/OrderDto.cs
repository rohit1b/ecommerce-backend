namespace ECommerce.Application.DTOs;

public class OrderItemDto
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}

public class OrderDto
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new();
}

public class CreateOrderItemDto
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderDto
{
    public string CustomerEmail { get; set; } = string.Empty;
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class UpdateOrderStatusDto
{
    public string Status { get; set; } = string.Empty;
}
