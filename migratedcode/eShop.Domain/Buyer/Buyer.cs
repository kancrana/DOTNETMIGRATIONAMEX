using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.Buyer
{
    public class Buyer : Entity<int>, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public string Name { get; private set; }

        private List<PaymentMethod> _paymentMethods;
        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        protected Buyer()
        {
            _paymentMethods = new List<PaymentMethod>();
        }

        public Buyer(string identity, string name) : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }
    }
}