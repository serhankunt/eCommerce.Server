using eCommerce.Server.Domain.Abstractions;
using eCommerce.Server.Domain.Shared;

namespace eCommerce.Server.Domain.Categories;

public sealed class Category : Entity
{
    public Name Name { get; set; } = new(string.Empty);
    public Guid? MainCategoryId { get; set; }
    public Category? MainCategory {  get; set; }
}
