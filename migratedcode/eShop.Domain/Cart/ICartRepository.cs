using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.Cart
{
    public interface ICartRepository
    {
        Task<CustomerCart> GetCartAsync(string customerId);
        CustomerCart GetCart(string customerId);
        IEnumerable<string> GetUsers();
        Task<CustomerCart> UpdateCartAsync(CustomerCart cart);
        CustomerCart UpdateCart(CustomerCart cart);
        Task<bool> DeleteCartAsync(string cartId);
        bool DeleteCart(string cartId);
    }
}