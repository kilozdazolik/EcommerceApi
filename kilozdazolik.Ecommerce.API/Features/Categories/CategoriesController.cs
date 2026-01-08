using Microsoft.AspNetCore.Mvc;

namespace kilozdazolik.Ecommerce.API.Features.Categories;

[ApiController]
[Route("[api/[controller]")]
public class CategoriesController(ICategoryService service) : ControllerBase
{
    // create
    
    //getall
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await service.GetCategoriesAsync();
        return Ok(categories);
    }
    
    //getbyid
    
    //update
    
    //delete
}