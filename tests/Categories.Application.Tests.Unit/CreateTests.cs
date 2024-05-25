using AutoMapper;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Domain.Categories;
using FluentAssertions;
using GenericRepository;
using MediatR;
using NSubstitute;
using System.Linq.Expressions;

namespace Categories.Application.Tests.Unit;

public class CreateTests
{
    private readonly CreateCategoryCommandHandler sut;
    private readonly ICategoryRepository categoryRepository = Substitute.For<ICategoryRepository>();//Mocklama iþlemi yapýldý
    private readonly IMapper mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();

    

    public CreateTests()
    {
        sut = new CreateCategoryCommandHandler(categoryRepository, mapper, unitOfWork);
    }
   

    [Fact]
    public async Task Create_ShouldIsSuccessfulReturnFalse_WhenNameAlreadyExists()
    {
        //Arrange => Tanýmlama 
        var command = new CreateCategoryCommand("ExistingName", null);
        categoryRepository.AnyAsync(Arg.Any<Expression<Func<Category,bool>>>()).Returns(true);

        //Act => Test etme
        var result = await sut.Handle(command,default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().HaveCount(1);
        result.ErrorMessages!.First().Should().Be("Category is already exist");
    }

    [Fact]
    public async Task Create_ShouldIsSuccessfulReturnTrue_WhenNameIsUnique()
    {
        //Arrange => Tanýmlama 
        var command = new CreateCategoryCommand("ExistingName", null);
        categoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(false);

        //Act => Test etme
        var result = await sut.Handle(command, default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Creating category is successful");
    }
}