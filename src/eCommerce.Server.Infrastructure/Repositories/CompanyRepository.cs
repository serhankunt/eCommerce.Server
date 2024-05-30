using eCommerce.Server.Domain.Companies;
using eCommerce.Server.Infrastructure.Context;
using GenericRepository;
using System.Linq.Expressions;

namespace eCommerce.Server.Infrastructure.Repositories;

public sealed class CompanyRepository : Repository<Company, ApplicationDbContext>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
    }

}
