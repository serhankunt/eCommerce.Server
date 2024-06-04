using AutoMapper;
using eCommerce.Server.Application;
using eCommerce.Server.Application.Behaviors;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Application.Features.Companies.CreateCompany;
using eCommerce.Server.Domain.Categories;
using eCommerce.Server.Domain.Companies;
using FluentAssertions;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Linq.Expressions;

namespace Companies.Application.Tests.Unit;

public class CreateTests
{
    private readonly IMediator sut;
    private readonly ICompanyRepository companyRepository = Substitute.For<ICompanyRepository>();//Mocklama iþlemi yapýldý
    private readonly IMapper mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IServiceProvider serviceProvider;
    private CreateCompanyCommand command;

    public CreateTests()
    {
        var services = new ServiceCollection();

        services.AddTransient(_ => companyRepository);
        services.AddTransient(_ => mapper);
        services.AddTransient(_ => unitOfWork);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        serviceProvider = services.BuildServiceProvider();
        sut = serviceProvider.GetRequiredService<IMediator>();

        command = new CreateCompanyCommand("Serhan Kunt LTD ÞTÝ",
           1,
           "13420057394",
           "Türkiye",
           "Ankara",
           "Etimesgut",
           "Þapka Devrimi",
           "Etimesgut/Ankara");
    }
    [Fact]
    public async Task Create_ShouldThrowException_WhenValidateFailure()
    {
        //Arrange
        command = new CreateCompanyCommand("Serhan Kunt LTD ÞTÝ",
           1,
           "134200",//Geçersiz vergi no
           "Türkiye",
           "Ankara",
           "Etimesgut",
           "Þapka Devrimi",
           "Etimesgut/Ankara");

        //Act
        Func<Task> act = async () => { await sut.Send(command); };

        //Assert
        await act.Should().ThrowAsync<ValidationException>();

    }

    [Fact]
    public async Task Create_ShouldIsSuccessfulReturnFalse_WhenTaxNumberAlreadyExists()
    {
       //Arrange
        companyRepository.AnyAsync(Arg.Any<Expression<Func<Company, bool>>>()).Returns(true);

        //Act => Test etme
        var result = await sut.Send(command, default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().HaveCount(1);
        result.ErrorMessages!.First().Should().Be("Tax number already exists");
    }

    [Fact]
    public async Task Create_ShouldIsSuccessfulReturnFalse_WhenTaxNumberIsUnique()
    {
        //Arrange
        companyRepository.AnyAsync(Arg.Any<Expression<Func<Company, bool>>>()).Returns(false);

        //Act => Test etme
        var result = await sut.Send(command, default);

        //Assert => Sonucu dönme
        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Create company is successful");
    }

}
