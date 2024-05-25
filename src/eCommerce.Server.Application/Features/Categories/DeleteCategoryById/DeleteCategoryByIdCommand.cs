using MediatR;
using TS.Result;

namespace eCommerce.Server.Application.Features.Categories.RemoveCategory;

public sealed record DeleteCategoryByIdCommand(
    Guid Id):IRequest<Result<string>>;
