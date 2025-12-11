using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Web.ViewModels
{
    public class ProductList
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<Product> Products { get; set; }
    }
}