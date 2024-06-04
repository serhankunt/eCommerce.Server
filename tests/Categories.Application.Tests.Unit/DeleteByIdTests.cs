using AutoMapper;
using eCommerce.Server.Application;
using eCommerce.Server.Application.Behaviors;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Application.Features.Categories.RemoveCategory;
using eCommerce.Server.Application.Features.Categories.UpdateCategory;
using eCommerce.Server.Domain.Categories;
using eCommerce.Server.Domain.Shared;
using FluentAssertions;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;
using TS.Result;

namespace Categories.Application.Tests.Unit;

public class DeleteByIdTests
{
    private readonly IMediator sut;
    private readonly ICategoryRepository categoryRepository = Substitute.For<ICategoryRepository>();//Mocklama iþlemi yapýldý
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IServiceProvider serviceProvider;
    private readonly Guid guid = Guid.NewGuid();

    public DeleteByIdTests()
    {
        var services = new ServiceCollection();

        services.AddTransient(_ => categoryRepository); 
        services.AddTransient(_ => unitOfWork);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        serviceProvider = services.BuildServiceProvider();
        sut = serviceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task DeleteById_ReturnIsSuccessfulFalse_WhenCategoryNotFound()
    {
        //Arrange
        var command = new DeleteCategoryByIdCommand(guid);
        categoryRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Category, bool>>>()).ReturnsNull();

        //Act
        var result = await sut.Send(command, default);

        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().HaveCount(1);
        result.ErrorMessages!.First().Should().Be("Category not found");
    }

    [Fact]
    public async Task DeleteById_ReturnIsSuccessfulTrue_WhenCategoryIsDeleted()
    {
        //Arrange
        var category = new Category { Id = guid, MainCategoryId = null,Name = new Name("Elektronik"), IsDeleted= false };
        var command = new DeleteCategoryByIdCommand(guid);

        categoryRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(category);

        //Act
        var result = await sut.Send(command, default);

        //Assert
        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Category deleted successfully");
    }
}
