using kilozdazolik.Ecommerce.API.Features.Categories;
using kilozdazolik.Ecommerce.API.Features.Products;
using kilozdazolik.Ecommerce.API.Features.Sales;
using Microsoft.EntityFrameworkCore;

namespace kilozdazolik.Ecommerce.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories {get; set;}
    public DbSet<Product> Products {get; set;}
    public DbSet<Sale> Sales {get; set;}
    public DbSet<SaleDetail> SaleDetails {get; set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SaleDetail>()
            .HasOne(sd => sd.Sale)
            .WithMany(s => s.SaleDetails)
            .HasForeignKey(sd => sd.SaleId);
        
        modelBuilder.Entity<SaleDetail>()
            .HasOne(sd => sd.Product)
            .WithMany(p => p.SaleDetails) 
            .HasForeignKey(sd => sd.ProductId);

        base.OnModelCreating(modelBuilder);
    }
}