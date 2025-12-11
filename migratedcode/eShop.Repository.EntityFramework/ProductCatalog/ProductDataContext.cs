using eShop.Domain.ProductCatalog;
using eShop.Repository.EntityFramework.ProductCatalog.EntityConfiguration;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Repository.EntityFramework.ProductCatalog
{
    public class ProductDataContext : DbContext
    {
        public ProductDataContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductBrand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }

        public ProductDataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeEntitiyConfiguration());
        }
    }
}