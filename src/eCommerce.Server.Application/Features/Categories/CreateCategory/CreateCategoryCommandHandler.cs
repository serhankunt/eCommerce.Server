using AutoMapper;
using eCommerce.Server.Domain.Categories;
using eCommerce.Server.Domain.Shared;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eCommerce.Server.Application.Features.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryExist = await categoryRepository.AnyAsync(p => p.Name == new Name(request.Name),cancellationToken);
        if (isCategoryExist)
        {
            return Result<string>.Failure("Category is already exist");
        }

        Category category = mapper.Map<Category>(request);

        await categoryRepository.AddAsync(category,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Creating category is successful");
    }
}
