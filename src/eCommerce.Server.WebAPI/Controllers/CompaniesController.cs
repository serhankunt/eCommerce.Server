using eCommerce.Server.Application.Features.Companies.CreateCompany;
using eCommerce.Server.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Server.WebAPI.Controllers;

public class CompaniesController : ApiController
{
    public CompaniesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request,cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

}
