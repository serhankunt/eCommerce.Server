using MediatR;
using TS.Result;

namespace eCommerce.Server.Application.Features.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    Guid? MainCategoryId):IRequest<Result<string>>;
