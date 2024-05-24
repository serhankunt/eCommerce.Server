using MediatR;
using TS.Result;

namespace eCommerce.Server.Application.Features.Categories.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name,
    Guid? MainCategoryId):IRequest<Result<string>>;
