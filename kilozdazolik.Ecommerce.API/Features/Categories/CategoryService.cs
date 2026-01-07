using kilozdazolik.Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;

namespace kilozdazolik.Ecommerce.API.Features.Categories;

public class CategoryService(AppDbContext dbContext) : ICategoryService
{
    public async Task CreateCategoryAsync(Category category)
    {
        throw new NotImplementedException();
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

    public async Task UpdateCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }
}