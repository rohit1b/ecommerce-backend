using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;

    public ItemService(IItemRepository repository)
    {
        _repository = repository;
    }

    private static ItemDto ToDto(Item i) => new()
    {
        Id = i.Id,
        Name = i.Name,
        Description = i.Description,
        Price = i.Price,
        Quantity = i.Quantity,
        Size = i.Size,
        ImageUrl = i.ImageUrl,
        CategoryId = i.CategoryId,
        CategoryName = i.Category?.Name ?? string.Empty
    };

    public async Task<List<ItemDto>> GetAllAsync()
    {
        var items = await _repository.GetAllWithCategoryAsync();
        return items.Select(ToDto).ToList();
    }

    public async Task<ItemDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdWithCategoryAsync(id);
        return item is null ? null : ToDto(item);
    }

    public async Task<ItemDto> CreateAsync(CreateItemDto dto)
    {
        var item = new Item
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Quantity = dto.Quantity,
            Size = dto.Size,
            ImageUrl = dto.ImageUrl,
            CategoryId = dto.CategoryId
        };

        await _repository.AddAsync(item);
        await _repository.SaveChangesAsync();

        var created = await _repository.GetByIdWithCategoryAsync(item.Id);
        return ToDto(created!);
    }

    public async Task<bool> UpdateAsync(UpdateItemDto dto)
    {
        var item = await _repository.GetByIdAsync(dto.Id);
        if (item is null) return false;

        item.Name = dto.Name;
        item.Description = dto.Description;
        item.Price = dto.Price;
        item.Quantity = dto.Quantity;
        item.Size = dto.Size;
        item.ImageUrl = dto.ImageUrl;
        item.CategoryId = dto.CategoryId;

        _repository.Update(item);
        return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item is null) return false;

        _repository.Delete(item);
        return await _repository.SaveChangesAsync();
    }

    public async Task<bool> AddQuantityAsync(AddQuantityDto dto)
    {
        var item = await _repository.GetByIdAsync(dto.ItemId);
        if (item is null) return false;

        item.Quantity += dto.Amount;
        _repository.Update(item);
        return await _repository.SaveChangesAsync();
    }
}
