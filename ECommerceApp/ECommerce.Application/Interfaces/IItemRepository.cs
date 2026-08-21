using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<List<Item>> GetAllWithCategoryAsync();
    Task<Item?> GetByIdWithCategoryAsync(int id);
}
