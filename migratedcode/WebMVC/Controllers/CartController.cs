using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using eShop.Web.Services;
using eShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<ActionResult> Index(string customerId)
        {
            var model = new CustomerCart();
            try
            {
                model = await _cartService.GetCart(customerId);
                Session["CartCount"] = model != null ? model.Items.Count : 0;
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
        public async Task<ActionResult> UpdateCart(CustomerCart cart)
        {
            var model = new CustomerCart();
            try
            {
                model = await _cartService.UpdateCart(cart);
            }
            catch (Exception ex)
            {
                // Log customized error messages
                string errorMessage = string.Format("{0} {1}", ex.Message, ex.StackTrace);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(errorMessage));
            }

            return Json(new { cart = model, cartCount = model.Items.Count }, JsonRequestBehavior.AllowGet);
        }
    }
}