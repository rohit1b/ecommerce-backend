using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces;

public interface IAdminUserRepository
{
    Task<AdminUser?> GetByEmailAsync(string email);
}
