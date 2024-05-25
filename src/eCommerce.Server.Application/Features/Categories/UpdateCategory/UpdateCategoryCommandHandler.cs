using AutoMapper;
using eCommerce.Server.Domain.Categories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eCommerce.Server.Application.Features.Categories.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByExpressionAsync(x=>x.Id == request.Id,cancellationToken);
        if (category is null) 
        {
            return Result<string>.Failure("Category not found");
        }

        if (category.Name.Value != request.Name)
        {
            var isCategoryNameExists = await categoryRepository.AnyAsync(y => y.Name == new Name(request.Name), cancellationToken);
            if (isCategoryNameExists)
            {
                return Result<string>.Failure("Category name already exists");
            }
        }

        if (request.MainCategoryId is not null)
        {
            if (request.Id == request.MainCategoryId)
            {
                return Result<string>.Failure("Main category cannot be itself");
            }
        }

        mapper.Map(request, category);  
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Updating category is successful");


    }
}
