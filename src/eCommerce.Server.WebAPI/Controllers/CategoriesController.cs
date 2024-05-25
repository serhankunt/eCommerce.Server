﻿using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Server.WebAPI.Controllers;

public class CategoriesController : ApiController
{
    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request,cancellationToken);

        return StatusCode(result.StatusCode, result);
    }
}