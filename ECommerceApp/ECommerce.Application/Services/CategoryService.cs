using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }).ToList();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category { Name = dto.Name };
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        return new CategoryDto { Id = category.Id, Name = category.Name };
    }

    public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null) return false;

        category.Name = dto.Name;
        _repository.Update(category);
        return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null) return false;

        _repository.Delete(category);
        return await _repository.SaveChangesAsync();
    }
}
