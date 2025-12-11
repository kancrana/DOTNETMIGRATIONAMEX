using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain
{
    public interface IRepository<T, Tid> : IReadOnlyRepository<T, Tid> where T : IAggregateRoot
    {
        void Save(T entity);
        void Add(T entity);
        void Delete(T entity);
    }
}