using eCommerce.Server.Domain.Categories;
using MediatR;
using NSubstitute;

namespace Categories.Application.Tests.Unit;

public class CreateTests
{
    private readonly IMediator _sut;
    private readonly ICategoryRepository categoryRepository = Substitute.For<ICategoryRepository>();//Mocklama iþlemi yapýldý

    public CreateTests()
    {
        
    }

    [Fact]
    public void Create_ShouldIsSuccessfulReturnFalse_WhenNameAlreadyExists()
    {
        //Arrange => Tanýmlama 


        //Act => Test etme

        //Assert => Sonucu dönme
    }
}