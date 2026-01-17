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

            if (product.Price < 0)
                throw new ArgumentException("Product price cannot be negative", nameof(product.Price));

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

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product is null) throw new KeyNotFoundException($"Product with id {id} not found");

            product.IsDeleted = true;

            await dbContext.SaveChangesAsync();
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

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int pageIndex, int pageSize)
        {
            return await  dbContext.Products
                .AsNoTracking()
                    .Where(p => !p.IsDeleted)
                    .OrderBy(p => p.Name)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new ProductDto(
                        p.Id,
                        p.Category.Id,     
                        p.Category.Name,  
                        p.Name,
                        p.Price,
                        p.IsDeleted
                    ))
                    .ToListAsync();
        }

        public async Task RestoreProductAsync(Guid id)
        {
            var product = await dbContext.Products
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                throw new KeyNotFoundException($"Product with id {id} not found");

            var category = await dbContext.Categories
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(c => c.Id == product.CategoryId);

            if (category is null || category.IsDeleted)
            {
                throw new InvalidOperationException("Cannot restore product because its category is deleted.");
            }

            product.IsDeleted = false;

            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product is null)
                throw new InvalidOperationException($"Product with id '{id}' not found");

            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            Helper.ValidateName(dto.Name, "Product name is required");

            if (dto.Price < 0)
                throw new ArgumentException("Product price cannot be negative", nameof(dto.Price));

            if (dto.CategoryId != product.CategoryId)
            {
                var category = await dbContext.Categories.FindAsync(dto.CategoryId);
                if (category is null || category.IsDeleted)
                {
                    throw new ArgumentException("Invalid Category ID.");
                }

                product.CategoryId = dto.CategoryId;

            }
            product.Name = dto.Name;
            product.Price = dto.Price;

            await dbContext.SaveChangesAsync();
        }
    }
}
