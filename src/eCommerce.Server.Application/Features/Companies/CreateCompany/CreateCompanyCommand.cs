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
    string Street,
    string FullAddress
    ):IRequest<Result<string>>;
