namespace kilozdazolik.Ecommerce.API.Features.Categories;

public interface ICategoryService
{
    Task CreateCategoryAsync(CreateCategoryDto category);
    Task<CategoryDto?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task UpdateCategoryAsync(Guid id, UpdateCategoryDto category);
    Task DeleteCategoryAsync(Guid id);
}