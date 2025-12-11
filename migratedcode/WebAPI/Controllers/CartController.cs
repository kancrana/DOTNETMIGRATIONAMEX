using eShop.Domain.Cart;
using eShop.Repository.EntityFramework.Cart;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eShop.WebApi.Controllers
{
[Route("api/[controller]")]
[ApiController]
    public class CartController : ControllerBase
    {
        ICartRepository _cartRepository;
        private static ILog log = LogManager.GetLogger(typeof(CartController));
        /// <summary>
        /// TODO Dependency Inject Reposotory Here
        /// </summary>
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CustomerCart>>GetCartById(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return Ok(cart);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<CustomerCart>>UpdateCartAsync(CustomerCart customerCart)
        {
            var cart = await _cartRepository.UpdateCartAsync(customerCart);
            return Ok(cart);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<bool> DeteleCartAsync(string id)
        {
            return await _cartRepository.DeleteCartAsync(id);
        }
    }
}