using eShop.Domain.ProductCatalog;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repository.EntityFramework.ProductCatalog.EntityConfiguration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(50);
            builder.Property(ci => ci.Price).IsRequired();
            builder.Property(ci => ci.PictureFileName).IsRequired();
            builder.Ignore(ci => ci.PictureUri);
            builder.HasOne(ci => ci.Brand).WithMany().HasForeignKey(ci => ci.ProductBrandId);
            builder.HasOne(ci => ci.ProductType).WithMany().HasForeignKey(ci => ci.ProductTypeId);
        }
    }
}