using AutoMapper;
using eCommerce.Server.Application;
using eCommerce.Server.Application.Behaviors;
using eCommerce.Server.Application.Features.Categories.GetAllCategory;
using eCommerce.Server.Domain.Categories;
using FluentAssertions;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace Categories.Application.Tests.Unit;

public class GetAllTests
{
    private readonly IMediator sut;
    private readonly ICategoryRepository categoryRepository = Substitute.For<ICategoryRepository>();//Mocklama iþlemi yapýldý
    private readonly IServiceProvider serviceProvider;

    public GetAllTests()
    {
        var services = new ServiceCollection();

        services.AddTransient(_ => categoryRepository);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        serviceProvider = services.BuildServiceProvider();
        sut = serviceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task GetAll_ShouldReturnEmpty_WhenCategoryListEmpty()
    {
        //Arrange
        var query = new GetAllCategoryQuery();
        var emptyCategoryList = new List<Category>().AsQueryable().BuildMock();

        categoryRepository
            .GetAll()
            .Returns(emptyCategoryList);

        //Act
        var result = await sut.Send(query);

        //Assert
        result.Data!.Count().Should().Be(0);
    }
}
