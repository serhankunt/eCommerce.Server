using eCommerce.Server.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eCommerce.Server.Application.Features.Categories.GetAllCategory;

public class GetAllCategoryQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoryQuery, Result<List<Category>>>
{
    public async Task<Result<List<Category>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository
            .GetAll()
            .Include(x=>x.MainCategory)
            .ToListAsync(cancellationToken);

        return new Result<List<Category>>(categories);
    }
}
