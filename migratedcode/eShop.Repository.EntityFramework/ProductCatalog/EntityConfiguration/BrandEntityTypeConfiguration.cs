using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eShop.Domain.ProductCatalog;
using Microsoft.EntityFrameworkCore;
namespace eShop.Repository.EntityFramework.ProductCatalog.EntityConfiguration
{
    public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.ToTable("ProductBrand");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        }
    }
}