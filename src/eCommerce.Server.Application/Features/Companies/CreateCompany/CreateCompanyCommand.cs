using eCommerce.Server.Application.Utilities;
using FluentValidation;
using MediatR;
using TS.Result;

namespace eCommerce.Server.Application.Features.Companies.CreateCompany;

public sealed record CreateCompanyCommand(
    string Name,
    int TaxDepartmantValue,
    string TaxNumber,
    string Country,
    string City,
    string Town,
    string Street):IRequest<Result<string>>;


public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(p => p.Name)
            .MinimumLength(3);

        RuleFor(p => p.TaxDepartmantValue)
            .TaxDepartmentValueMustBeValid();

        RuleFor(p => p.TaxNumber)
            .TaxNumberMustBeValid();

        RuleFor(p => p.Country)
            .MinimumLength(3);

        RuleFor(p => p.City)
            .MinimumLength(3);

        RuleFor(p => p.Town)
            .MinimumLength(3);

    }
}

public sealed class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result<string>>
{
    public Task<Result<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
