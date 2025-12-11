using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.Cart
{
    public class CartCheckout
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardSecutityNumber { get; set; }
        public string CardTypeId { get; set; }
        public string Buyer { get; set; }
        public Guid RequestId { get; set; }
    }
}