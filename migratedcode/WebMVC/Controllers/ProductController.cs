using Elmah;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.Web.Services;
using eShop.Web.ViewModels;
using eShop.Web.ViewModels.ProductListViewModel;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductListingService _productListingService;
        private readonly ICartService _cartService;
        public ProductController(IProductListingService productListingService, ICartService cartService)
        {
            _productListingService = productListingService;
            _cartService = cartService;
        }

        public async Task<ActionResult> Index()
        {
            Session["CustomerId"] = "1";
            var model = new ProductListViewModel();
            try
            {
                var productList = new ProductList();
                productList.Products = await _productListingService.GetProductList();
                model.ProductList = productList;
                model.ProductBrands = await _productListingService.GetProductBrancdList();
                model.ProductTypes = await _productListingService.GetProductTypeList();
                var cart = await _cartService.GetCart(Session.GetString("CustomerId"));
                Session["CartCount"] = cart != null ? cart.Items.Count : 0;
            }
            catch (Exception ex)
            {
                // Log customized error messages
                string errorMessage = string.Format("{0} {1}", ex.Message, ex.StackTrace);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(errorMessage));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _productListingService.FindById(productId);
            var cart = await _cartService.GetCart(Session.GetString("CustomerId"));
            bool isItemAdded = false;
            try
            {
                if (cart != null && cart.Items.Count > 0)
                {
                    var item = cart.Items.FirstOrDefault(x => x.ProductId == product.Id);
                    if (item != null)
                    {
                        item.Quantity = item.Quantity + quantity;
                        cart = await _cartService.UpdateCart(cart);
                        isItemAdded = true;
                    }
                }

                if (!isItemAdded)
                {
                    cart = (cart != null ? cart : new CustomerCart());
                    cart.BuyerId = "1";
                    var cartItem = new CartItem();
                    cartItem.ProductId = product.Id;
                    cartItem.ProductName = product.Name;
                    cartItem.Quantity = 1;
                    cartItem.UnitPrice = product.Price;
                    cartItem.PictureUrl = product.PictureFileName;
                    cartItem.OldUnitPrice = product.OldPrice;
                    cart.Items.Add(cartItem);
                    cart = await _cartService.UpdateCart(cart);
                    isItemAdded = true;
                }
            }
            catch (Exception ex)
            {
                // Log customized error messages
                string errorMessage = string.Format("{0} {1}", ex.Message, ex.StackTrace);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(errorMessage));
            }

            if (isItemAdded)
            {
                Session["CartCount"] = cart.Items.Count;
                return Json(new { isSuccessfull = true, cart = cart, cartCount = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { isSuccessfull = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> ProductFilter(int? brandId, int? typeId)
        {
            var productList = new ProductList();
            try
            {
                var products = await _productListingService.GetProductList();
                if (products.Count > 0)
                {
                    if (brandId != null && typeId != null)
                    {
                        productList.Products = products.Where(x => x.ProductBrandId == brandId && x.ProductTypeId == typeId).ToList();
                    }
                    else
                    {
                        productList.Products = brandId > 0 ? products.Where(x => x.ProductBrandId == brandId).ToList() : typeId > 0 ? products.Where(x => x.ProductTypeId == typeId).ToList() : products;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log customized error messages
                string errorMessage = string.Format("{0} {1}", ex.Message, ex.StackTrace);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(errorMessage));
            }

            return PartialView("ProductList", productList);
        }

        public ActionResult SuccessNotification(string message)
        {
            return PartialView("_SuccessNotification", message);
        }

        public ActionResult ErrorNotification(string message)
        {
            return PartialView("_ErrorNotification", message);
        }
    }
}