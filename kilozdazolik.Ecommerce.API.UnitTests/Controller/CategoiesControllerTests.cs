using FakeItEasy;
using FakeItEasy.Configuration;
using FluentAssertions;
using kilozdazolik.Ecommerce.API.Features.Categories;
using Microsoft.AspNetCore.Mvc;

namespace kilozdazolik.Ecommerce.API.UnitTests.Controller;

public class CategoriesControllerTests
{
    private readonly ICategoryService _service;
    private readonly CategoriesController _controller;

    public CategoriesControllerTests()
    {
        _service = A.Fake<ICategoryService>();
        _controller = new CategoriesController(_service);
    }
    
    [Fact]
    public async Task GetCategories_ReturnOK()
    {
        //Arrange
        var categories = new List<CategoryDto>
        {
            new CategoryDto(Guid.NewGuid(), "Category 1"),
            new CategoryDto(Guid.NewGuid(), "Category 2"),
            new CategoryDto(Guid.NewGuid(), "Category 3")
        };
        A.CallTo(() => _service.GetCategoriesAsync()).Returns(categories);
        
        //Act
        var result = await _controller.GetCategories();
        
        //Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedCategories = okResult.Value.Should().BeAssignableTo<IEnumerable<CategoryDto>>().Subject;
        returnedCategories.Should().BeEquivalentTo(categories);
        A.CallTo(() => _service.GetCategoriesAsync()).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task GetCategoryById_ReturnOK_WhenCategoryExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var category = new CategoryDto(id, "Existing Category");
        
        A.CallTo(() => _service.GetCategoryByIdAsync(id)).Returns(category);

        // Act
        var result = await _controller.GetCategoryById(id);

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(category);
    }

    [Fact]
    public async Task GetCategoryById_ReturnNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        A.CallTo(() => _service.GetCategoryByIdAsync(id)).Returns((CategoryDto?)null);

        // Act
        var result = await _controller.GetCategoryById(id);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task CreateCategory_ReturnCreated()
    {
        // Arrange
        var createDto = new CreateCategoryDto("New Category"); 
        var createdCategory = new CategoryDto(Guid.NewGuid(), "New Category");

        A.CallTo(() => _service.CreateCategoryAsync(createDto)).Returns(createdCategory);

        // Act
        var result = await _controller.CreateCategory(createDto);

        // Assert
        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.RouteValues!["id"].Should().Be(createdCategory.Id);
        createdResult.Value.Should().BeEquivalentTo(createdCategory);
    }

    [Fact]
    public async Task UpdateCategory_ReturnNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateDto = new UpdateCategoryDto("Updated Name"); // Igazítsd a DTO-hoz
        
        A.CallTo(() => _service.UpdateCategoryAsync(id, updateDto)).DoesNothing();

        // Act
        var result = await _controller.UpdateCategory(id, updateDto);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        A.CallTo(() => _service.UpdateCategoryAsync(id, updateDto)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteCategory_ReturnNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();
        A.CallTo(() => _service.DeleteCategoryAsync(id)).DoesNothing();

        // Act
        var result = await _controller.DeleteCategory(id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        A.CallTo(() => _service.DeleteCategoryAsync(id)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task RestoreCategory_ReturnNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();
        A.CallTo(() => _service.RestoreCategoryAsync(id)).DoesNothing();

        // Act
        var result = await _controller.RestoreCategory(id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        A.CallTo(() => _service.RestoreCategoryAsync(id)).MustHaveHappenedOnceExactly();
    }
}