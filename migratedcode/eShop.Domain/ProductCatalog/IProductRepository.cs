using eShop.Domain.ProductCatalog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.ProductCatalog
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<PaginatedViewModel<Product>> SearchByNameAsync(string name, int pageSize, int pageIndex);
        PaginatedViewModel<Product> SearchByName(string name, int pageSize, int pageIndex);
        Task<PaginatedViewModel<Product>> SearchByTypeAndBrandAsync(int typeId, int? brandId, int pageSize, int pageIndex);
        PaginatedViewModel<Product> SearchByTypeAndBrand(int typeId, int? brandId, int pageSize, int pageIndex);
        Task<IEnumerable<ProductBrand>> GetProductBrandsAsync();
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
        void BulkInsert(IEnumerable<Product> products);
    }
}