using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain
{
    public interface IReadOnlyRepository<T, Tid>
        where T : IAggregateRoot
    {
        T FindBy(Tid id);
        IEnumerable<T> FindAll();
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> FindByIdAsync(Tid id);
    }
}