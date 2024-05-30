using AutoMapper;
using eCommerce.Server.Application.Features.Categories.CreateCategory;
using eCommerce.Server.Application.Features.Categories.UpdateCategory;
using eCommerce.Server.Domain.Categories;
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
    }
}
