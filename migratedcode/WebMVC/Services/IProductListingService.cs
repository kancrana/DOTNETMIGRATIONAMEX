using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.Web.ViewModels;

namespace eShop.Web.Services
{
    public interface IProductListingService
    {
        Task<ProductList> GetProductList(int page, int take, int? brand, int? type);
        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetProductTypes();
        Task<List<Product>> GetProductList();
        Task<List<ProductBrand>> GetProductBrancdList();
        Task<List<ProductType>> GetProductTypeList();
        Task<Product> FindById(int Id);
    }
}