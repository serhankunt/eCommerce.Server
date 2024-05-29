using AutoMapper;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Application.Features.Categories.UpdateCategory;
using eCommerce.Server.Domain.Categories;
using FluentAssertions;
using GenericRepository;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace Categories.Application.Tests.Unit;

public class UpdateTests
{
    private readonly UpdateCategoryCommandHandler sut;
    private readonly ICategoryRepository categoryRepository = Substitute.For<ICategoryRepository>();//Mocklama iþlemi yapýldý
    private readonly IMapper mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();

    public UpdateTests()
    {
        sut = new UpdateCategoryCommandHandler(categoryRepository,mapper,unitOfWork);
    }

    [Fact]
    public async Task Update_ReturnIsSuccessfulFalse_WhenCategoryNotFound()
    {
        //Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), "Elektronik", null);
        categoryRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Category, bool>>>()).ReturnsNull();

        //Act
        var result = await sut.Handle(command, default);

        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().HaveCount(1);
        result.ErrorMessages!.First().Should().Be("Category not found");
    }

    [Fact]
    public async Task Update_ShouldIsSuccessfulReturnFalse_WhenNameAlreadyExists()
    {
        //Arrange => Tanýmlama 
        var guid = Guid.NewGuid();
        var command = new UpdateCategoryCommand(guid, "Elektronik", null);
        
        var existingCategory = new Category {Id= guid, Name = new Name("Existing Category") };

        categoryRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(existingCategory);
        categoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(true);

        //Act => Test etme
        var result = await sut.Handle(command, default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().HaveCount(1);
        result.ErrorMessages!.First().Should().Be("Category name already exists");
    }

    [Fact]
    public async Task Update_ShouldIsSuccessfulReturnFalse_WhenMainCategoryIdEqualsId()
    {
        //Arrange => Tanýmlama 
        var guid = Guid.NewGuid();
        var command = new UpdateCategoryCommand(guid, "Elektronik", guid);

        var existingCategory = new Category { Id = guid, Name = new Name("Existing Category") };

        categoryRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(existingCategory);
        categoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(false);

        //Act => Test etme
        var result = await sut.Handle(command, default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().HaveCount(1);
        result.ErrorMessages!.First().Should().Be("Main category cannot be itself");
    }

    [Fact]
    public async Task Update_ShouldBeUpdateCategory_WhenNameDoesNotExistsAndMainCategoryIdDoesNotEqualsId()
    {
        //Arrange => Tanýmlama 
        var guid = Guid.NewGuid();
        var command = new UpdateCategoryCommand(guid, "Elektronik", null);

        var existingCategory = new Category { Id = guid, Name = new Name("Existing Category") };

        categoryRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(existingCategory);
        categoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(false);

        //Act => Test etme
        var result = await sut.Handle(command, default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeTrue();
        
        result.Data.Should().Be("Updating category is successful");
    }
}
