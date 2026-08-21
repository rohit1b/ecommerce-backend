using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>()
            .Property(i => i.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Price)
            .HasColumnType("decimal(18,2)");

        // Seed categories, matching the reference admin panel
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "GentsWear" },
            new Category { Id = 2, Name = "LadiesWear" },
            new Category { Id = 3, Name = "MeansWear" },
            new Category { Id = 4, Name = "ChildrenWear" }
        );

        // Seed one admin login (email: admin@shoeshop.com / password: Admin@123)
        modelBuilder.Entity<AdminUser>().HasData(
            new AdminUser
            {
                Id = 1,
                Email = "admin@shoeshop.com",
                PasswordHash = AuthService.HashPassword("Admin@123")
            }
        );
    }
}
