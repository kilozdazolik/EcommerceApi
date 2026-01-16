namespace kilozdazolik.Ecommerce.API.Features.Products
{
    public record ProductDto(
    Guid Id,
    Guid CategoryId,
    string CategoryName,
    string Name,
    decimal Price,
    bool IsDeleted);

    public record CreateProductDto (
        Guid CategoryId,
        string Name,
        decimal Price);

    public record UpdateProductDto (
        Guid CategoryId,
        string Name,
        decimal Price);
}
