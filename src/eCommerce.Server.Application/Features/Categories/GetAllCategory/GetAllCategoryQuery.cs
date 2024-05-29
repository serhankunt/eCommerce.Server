using eCommerce.Server.Domain.Categories;
using MediatR;
using TS.Result;

namespace eCommerce.Server.Application.Features.Categories.GetAllCategory;

public class GetAllCategoryQuery : IRequest<Result<List<Category>>>
{
}
