namespace kilozdazolik.Ecommerce.API.Features.Categories;

public interface ICategoryService
{
    Task CreateCategoryAsync(Category category);
    Task<CategoryDto?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}