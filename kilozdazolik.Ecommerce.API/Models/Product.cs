namespace kilozdazolik.Ecommerce.API.Models;

public class Product
{
    public Guid Id {get; set;}
    public Guid CategoryId {get; set;}
    public Category Category {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
}