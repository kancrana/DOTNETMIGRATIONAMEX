using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Web.ViewModels
{
    public class CustomerCart
    {
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalItems { get; set; }
        public decimal TotalPrice { get; set; }

        public CustomerCart()
        {
        }

        public CustomerCart(string customerId)
        {
            this.BuyerId = customerId;
        }
    }
}