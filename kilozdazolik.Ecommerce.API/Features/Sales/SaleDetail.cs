using kilozdazolik.Ecommerce.API.Features.Products;

namespace kilozdazolik.Ecommerce.API.Features.Sales;

public class SaleDetail
{
    public Guid Id {get; set;}
    public Guid SaleId {get; set;}
    public Sale Sale {get; set;}
    public Guid ProductId {get; set;}
    public Product Product {get; set;}
    public byte Quantity {get; set;}
    public decimal UnitPrice {get; set;}
}