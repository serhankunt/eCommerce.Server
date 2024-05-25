using AutoMapper;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Application.Features.Categories.UpdateCategory;
using eCommerce.Server.Domain.Categories;

namespace eCommerce.Server.Application.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        //CreateMap<CreateCategoryCommand, Category>()
        //    .ForMember(dest=> dest.Name, opt=>opt.MapFrom(src=> new Name(src.Name)));

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
    }
}
