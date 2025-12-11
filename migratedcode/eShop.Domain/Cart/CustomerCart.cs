using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.Cart
{
    public class CustomerCart
    {
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public CustomerCart()
        {
        }

        public CustomerCart(string customerId)
        {
            this.BuyerId = customerId;
        }
    }
}