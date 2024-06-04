using AutoMapper;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Application.Features.Categories.UpdateCategory;
using eCommerce.Server.Application.Features.Companies.CreateCompany;
using eCommerce.Server.Domain.Categories;
using eCommerce.Server.Domain.Companies;
using eCommerce.Server.Domain.Shared;

namespace eCommerce.Server.Application.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCategoryCommand, Category>()
            .ForMember(x => x.Name, options =>
            {
                options.MapFrom(y=> new Name(y.Name));
            });

        CreateMap<UpdateCategoryCommand, Category>()
            .ForMember(x => x.Name, options =>
            {
                options.MapFrom(y => new Name(y.Name));
            });

        CreateMap<CreateCompanyCommand, Company>()
            .ForMember(p => p.Name, options =>
            {
                options.MapFrom(p => new Name(p.Name));
            })
            .ForMember(p => p.TaxNumber, options =>
            {
                options.MapFrom(p => new TaxNumber(p.TaxNumber));
            })
             .ForMember(p => p.TaxDepartmant, options =>
             {
                 options.MapFrom(p =>TaxDepartmantSmartEnum.FromValue(p.TaxDepartmantValue));
             })
             .ForMember(p => p.Address, options =>
             {
                 options.MapFrom(p => new Address(p.Country, p.City, p.Town, p.Street, p.FullAddress));
             });
      
    }
}
