using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;

    public OrderService(IOrderRepository orderRepository, IItemRepository itemRepository)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
    }

    private static OrderDto ToDto(Order o) => new()
    {
        Id = o.Id,
        CustomerEmail = o.CustomerEmail,
        OrderDate = o.OrderDate,
        Status = o.Status,
        TotalPrice = o.OrderItems.Sum(oi => oi.Price * oi.Quantity),
        OrderItems = o.OrderItems.Select(oi => new OrderItemDto
        {
            ItemId = oi.ItemId,
            ItemName = oi.ItemName,
            Quantity = oi.Quantity,
            Price = oi.Price,
            TotalPrice = oi.Price * oi.Quantity
        }).ToList()
    };

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllWithItemsAsync();
        return orders.Select(ToDto).ToList();
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(id);
        return order is null ? null : ToDto(order);
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            CustomerEmail = dto.CustomerEmail,
            OrderDate = DateTime.UtcNow,
            Status = "Pending"
        };

        foreach (var line in dto.Items)
        {
            var item = await _itemRepository.GetByIdAsync(line.ItemId);
            if (item is null) continue;

            order.OrderItems.Add(new OrderItem
            {
                ItemId = item.Id,
                ItemName = item.Name,
                Quantity = line.Quantity,
                Price = item.Price
            });
        }

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        var created = await _orderRepository.GetByIdWithItemsAsync(order.Id);
        return ToDto(created!);
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null) return false;

        order.Status = status;
        _orderRepository.Update(order);
        return await _orderRepository.SaveChangesAsync();
    }
}
