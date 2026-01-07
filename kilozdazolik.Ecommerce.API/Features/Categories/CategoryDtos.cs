namespace kilozdazolik.Ecommerce.API.Features.Categories;

public record CategoryDto(Guid Id, string Name);
public record CreateCategoryDto(string Name);