using eCommerce.Server.Domain.Companies;
using eCommerce.Server.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace eCommerce.Server.Infrastructure.Configurations;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder
            .Property(p => p.Name)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired()
            .HasColumnType("varchar(50)");
        
        builder
            .OwnsOne(p => p.Address, a =>
            {
                a.Property(p => p.Country).HasColumnName("Country").HasColumnType("varchar(50)");
                a.Property(p => p.City).HasColumnName("City").HasColumnType("varchar(50)");
                a.Property(p => p.Town).HasColumnName("Town").HasColumnType("varchar(50)");
                a.Property(p => p.Street).HasColumnName("Street").HasColumnType("varchar(50)");
                a.Property(p => p.FullAddress).HasColumnName("FullAddress").HasColumnType("varchar(250)");
            });

        builder
            .Property(p=>p.TaxNumber)
            .HasConversion(p=>p.Value, v=> new TaxNumber(v))
            .HasColumnName("TaxNumber");

        builder
            .Property(p => p.TaxDepartmant)
            .HasMaxLength(11)
            .HasConversion(v => v.Value,
                            v => TaxDepartmantSmartEnum.FromValue(v))
            .HasColumnName("TaxDepartment")
            .IsRequired();




        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}