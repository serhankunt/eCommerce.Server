using eCommerce.Server.Application.Utilities;
using FluentValidation;

namespace eCommerce.Server.Application.Features.Companies.CreateCompany;

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
