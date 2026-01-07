namespace kilozdazolik.Ecommerce.API.Features.Sales;

public class Sale
{
    public Guid Id {get; set;}
    public List<SaleDetail> SaleDetails { get; set; } = new();
    public decimal TotalAmount {get; set;}
    public DateTime Date {get; set;}
}