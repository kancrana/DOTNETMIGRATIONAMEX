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
    public class ProductTypeEntitiyConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductType");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).IsRequired();
            builder.Property(cb => cb.Name).IsRequired().HasMaxLength(100);
        }
    }
}