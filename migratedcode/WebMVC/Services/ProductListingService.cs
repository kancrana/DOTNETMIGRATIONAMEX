using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using eShop.Web.Infrastructure;
using eShop.Web.ViewModels;

namespace eShop.Web.Services
{
    public class ProductListingService : HttpClientBase, IProductListingService
    {
        private readonly AppSettings _appSettings;
        public ProductListingService(AppSettings appSettings)
        {
            this._appSettings = appSettings;
        // Get the Base url for Product listing API 
        }

        public Task<IEnumerable<SelectListItem>> GetBrands()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductList> GetProductList(int page, int take, int? brand, int? type)
        {
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath("<CustomAPICallPath>").HttpMethod(HttpMethod.Get).GetHttpMessage();
            return await SendRequest<ProductList>(response);
        }

        public Task<IEnumerable<SelectListItem>> GetProductTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProductList()
        {
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath("api/ProductCatalog/items").HttpMethod(HttpMethod.Get).GetHttpMessage();
            return await SendRequest<List<Product>>(response);
        }

        public async Task<List<ProductBrand>> GetProductBrancdList()
        {
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath("api/ProductCatalog/productBrands").HttpMethod(HttpMethod.Get).GetHttpMessage();
            return await SendRequest<List<ProductBrand>>(response);
        }

        public async Task<List<ProductType>> GetProductTypeList()
        {
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath("api/ProductCatalog/productTypes").HttpMethod(HttpMethod.Get).GetHttpMessage();
            return await SendRequest<List<ProductType>>(response);
        }

        public async Task<Product> FindById(int Id)
        {
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath("api/ProductCatalog/items/" + Id).HttpMethod(HttpMethod.Get).GetHttpMessage();
            return await SendRequest<Product>(response);
        }
    }
}