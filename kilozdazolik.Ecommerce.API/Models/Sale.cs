namespace kilozdazolik.Ecommerce.API.Models;

public class Sale
{
    public Guid Id {get; set;}
    public List<SaleDetail> SaleDetails {get; set;}
    public decimal TotalAmount {get; set;}
    public DateTime Date {get; set;}
}