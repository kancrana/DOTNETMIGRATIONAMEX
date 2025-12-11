using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.Web.ViewModels;

namespace eShop.Web.Services
{
    public interface ICartService
    {
        Task<CustomerCart> GetCart(string customerId);
        Task<CustomerCart> UpdateCart(CustomerCart cart);
        Task<bool> DeleteCart(string cartId);
    }
}