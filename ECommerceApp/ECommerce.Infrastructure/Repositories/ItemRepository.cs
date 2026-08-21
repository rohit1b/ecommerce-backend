using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Item>> GetAllWithCategoryAsync() =>
        await _dbSet.Include(i => i.Category).ToListAsync();

    public async Task<Item?> GetByIdWithCategoryAsync(int id) =>
        await _dbSet.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
}
