using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.Buyer
{
    public interface IBuyerRepository : IReadOnlyRepository<Buyer, int>
    {
        Buyer Add(Buyer entity);
        Buyer Update(Buyer entity);
        //Task<Buyer> FindAsync(string BuyerIdentityGuid);
        Task<Buyer> FindByIdAsync(string id);
    }
}