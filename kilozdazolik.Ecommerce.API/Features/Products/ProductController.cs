using Microsoft.AspNetCore.Mvc;

namespace kilozdazolik.Ecommerce.API.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService service) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto product)
        {
            var createdProduct = await service.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await service.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }

}
