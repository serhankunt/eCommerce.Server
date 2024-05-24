using eCommerce.Server.Domain.Categories;
using eCommerce.Server.Infrastructure.Context;
using GenericRepository;

namespace eCommerce.Server.Infrastructure.Repositories;

public sealed class CategoryRepository : Repository<Category, ApplicationDbContext>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
