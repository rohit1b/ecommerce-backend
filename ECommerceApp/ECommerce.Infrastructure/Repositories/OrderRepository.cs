using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetAllWithItemsAsync() =>
        await _dbSet.Include(o => o.OrderItems).ToListAsync();

    public async Task<Order?> GetByIdWithItemsAsync(int id) =>
        await _dbSet.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
}
