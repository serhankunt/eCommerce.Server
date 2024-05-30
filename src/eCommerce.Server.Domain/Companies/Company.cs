using eCommerce.Server.Domain.Abstractions;
using eCommerce.Server.Domain.Shared;

namespace eCommerce.Server.Domain.Companies;

public sealed class Company : Entity
{
    public Name Name { get; set; } = default!;
    public TaxDepartmantSmartEnum TaxDepartmant { get; set; } = TaxDepartmantSmartEnum.Beypazarý;
    public TaxNumber TaxNumber { get; set; } = default!;
    public Address Address { get; set; } = default!;
}
