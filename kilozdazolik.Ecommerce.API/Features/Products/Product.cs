using kilozdazolik.Ecommerce.API.Features.Categories;
using kilozdazolik.Ecommerce.API.Features.Sales;

namespace kilozdazolik.Ecommerce.API.Features.Products;

public class Product
{
    public Guid Id {get; set;}
    public Guid CategoryId {get; set;}
    public Category Category {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
    public bool IsDeleted {get; set;}
    public List<SaleDetail> SaleDetails { get; set; } = new();
}