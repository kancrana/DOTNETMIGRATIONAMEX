using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.ProductCatalog.ViewModel
{
    public class PaginatedViewModel<TEntity>
        where TEntity : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; private set; }

        public PaginatedViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            Data = data;
        }
    }
}