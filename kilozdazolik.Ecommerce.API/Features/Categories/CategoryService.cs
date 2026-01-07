using kilozdazolik.Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Internal;

namespace kilozdazolik.Ecommerce.API.Features.Categories;

public class CategoryService(AppDbContext dbContext) : ICategoryService
{
    public async Task CreateCategoryAsync(CreateCategoryDto category)
    {
        if (category is null) throw new ArgumentNullException(nameof(category));
        
        if (String.IsNullOrWhiteSpace(category.Name)) throw new  ArgumentException("Category name is required", nameof(category.Name));

        bool exists = await dbContext.Categories
            .AnyAsync(c => c.Name == category.Name && !c.IsDeleted);

        if (exists)
            throw new InvalidOperationException($"A category with name '{category.Name}' already exists.");

        Category newCategory = new()
        {
            Id = Guid.NewGuid(),
            Name = category.Name,
            IsDeleted = false
        };
        
        dbContext.Categories.Add(newCategory);
        await dbContext.SaveChangesAsync();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
    {
        return await dbContext.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryDto(c.Id,  c.Name))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        return await dbContext.Categories
            .OrderByDescending(c => c.Name)
            .Select(c => new CategoryDto(c.Id,  c.Name))
            .ToListAsync();
    }

    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Category name is required", nameof(dto.Name));

        var category = await dbContext.Categories.FindAsync(id);

        if (category is null)
            throw new InvalidOperationException($"Category with id '{id}' not found");

        category.Name = dto.Name;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await dbContext.Categories.FindAsync(id);

        if (category is null) throw new KeyNotFoundException($"Category with id {id} not found");

        category.IsDeleted = true;

        await dbContext.SaveChangesAsync();
    }
}