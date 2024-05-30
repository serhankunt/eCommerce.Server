using FluentValidation.Validators;
using FluentValidation;
using eCommerce.Server.Domain.Companies;

namespace eCommerce.Server.Application.Utilities;

public class TaxDepartmentValueMustBeValid<T> : PropertyValidator<T,int>
{
    public override string Name => "TaxDepartmentValueMustBeValid";

    public TaxDepartmentValueMustBeValid()
    {
        
    }

    public override bool IsValid(ValidationContext<T> context, int value)
    {
        var valid = TaxDepartmantSmartEnum.List.Any(taxDepartment => taxDepartment.Value == value);
        return valid;   
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} must be a valid tax department value";
    }
}