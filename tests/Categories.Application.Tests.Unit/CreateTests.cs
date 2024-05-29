using AutoMapper;
using eCommerce.Server.Application.Behaviors;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Domain.Categories;
using FluentAssertions;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Linq.Expressions;
using TS.Result;

namespace Categories.Application.Tests.Unit;

public class CreateTests
{
    private readonly IMediator sut;
    private readonly ICategoryRepository categoryRepository = Substitute.For<ICategoryRepository>();//Mocklama iþlemi yapýldý
    private readonly IMapper mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IServiceProvider serviceProvider;

    public CreateTests()
    {
        var services = new ServiceCollection();

        services.AddTransient(_ => categoryRepository);
        services.AddTransient(_ => mapper);
        services.AddTransient(_ => unitOfWork);
        services.AddTransient<IRequestHandler<CreateCategoryCommand, Result<string>>, CreateCategoryCommandHandler>();

        services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateCategoryCommand>();
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        serviceProvider = services.BuildServiceProvider();
        sut = serviceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task Create_ShouldThrowException_WhenValidateFailure()
    {
        //Arrange
        var command = new CreateCategoryCommand("", null);

        //Act
        Func<Task> act = async () => { await sut.Send(command); };

        //Assert
        await act.Should().ThrowAsync<ValidationException>();

    }
   

    [Fact]
    public async Task Create_ShouldIsSuccessfulReturnFalse_WhenNameAlreadyExists()
    {
        //Arrange => Tanýmlama 
        var command = new CreateCategoryCommand("ExistingName", null);
        categoryRepository.AnyAsync(Arg.Any<Expression<Func<Category,bool>>>()).Returns(true);

        //Act => Test etme
        var result = await sut.Send(command,default);

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
        var result = await sut.Send(command, default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Creating category is successful");
    }
}