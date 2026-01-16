using kilozdazolik.Ecommerce.API.Data;
using kilozdazolik.Ecommerce.API.Features.Categories;
using kilozdazolik.Ecommerce.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace kilozdazolik.Ecommerce.API.Features.Products
{
    public class ProductService(AppDbContext dbContext) : IProductService
    {
        public async Task<ProductDto> CreateProductAsync(CreateProductDto product)
        {
            ArgumentNullException.ThrowIfNull(product);
            Helper.ValidateName(product.Name, "Product name is required");

            var category = await dbContext.Categories.FindAsync(product.CategoryId);

            if ( category is null || category.IsDeleted)
            {
                throw new ArgumentException("Invalid Category ID.");
            }

            Product newProduct = new()
            {
                Id = Guid.NewGuid(),
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price,
                IsDeleted = false
            };

            dbContext.Products.Add(newProduct);
            await dbContext.SaveChangesAsync();

            return new ProductDto(
                        newProduct.Id,
                        category.Id,
                        category.Name,
                        newProduct.Name,
                        newProduct.Price,
                        newProduct.IsDeleted
                    );
        }

        public Task DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            return await dbContext.Products
                    .Where(p => p.Id == id)
                    .Select(p => new ProductDto(
                        p.Id,
                        p.Category.Id,     
                        p.Category.Name,  
                        p.Name,
                        p.Price,
                        p.IsDeleted
                    ))
                    .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RestoreProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Guid id, UpdateProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
