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
    public class CartService : HttpClientBase, ICartService
    {
        private readonly AppSettings _appSettings;
        public CartService(AppSettings appSettings)
        {
            this._appSettings = appSettings;
        }

        public Task<bool> DeleteCart(string cartId)
        {
            string apiPath = "api/cart/" + cartId;
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath(apiPath).HttpMethod(HttpMethod.Delete).GetHttpMessage();
            return SendRequest(response);
        }

        public async Task<CustomerCart> GetCart(string customerId)
        {
            string apiPath = "api/cart/" + customerId;
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath(apiPath).HttpMethod(HttpMethod.Get).GetHttpMessage();
            return CreateCartViewModel(await SendRequest<CustomerCart>(response));
        }

        public async Task<CustomerCart> UpdateCart(CustomerCart cart)
        {
            string apiPath = "api/cart/update/";
            string json = JsonConvert.SerializeObject(cart);
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = new HttpRequestBuilder(_appSettings.ApiBaseUrl).SetPath(apiPath).Content(httpContent).HttpMethod(HttpMethod.Post).GetHttpMessage();
            return CreateCartViewModel(await SendRequest<CustomerCart>(response));
        }

        public CustomerCart CreateCartViewModel(CustomerCart customerCart)
        {
            if (customerCart != null)
            {
                customerCart.TotalItems = customerCart.Items.Count();
                customerCart.TotalPrice = customerCart.Items.Select(x => x.Quantity * x.UnitPrice).Sum();
            }

            return customerCart;
        }
    }
}