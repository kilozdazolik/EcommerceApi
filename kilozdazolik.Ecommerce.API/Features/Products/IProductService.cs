namespace kilozdazolik.Ecommerce.API.Features.Products
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(CreateProductDto product);
        Task<ProductDto?> GetProductByIdAsync(Guid id, int pageIndex = 1 , int pageSize = 10);
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task UpdateProductAsync(Guid id, UpdateProductDto product);
        Task DeleteProductAsync(Guid id);
        Task RestoreProductAsync(Guid id);
    }
}
