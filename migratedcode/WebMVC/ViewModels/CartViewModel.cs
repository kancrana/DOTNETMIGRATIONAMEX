using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Web.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string Imageurl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Unitprice { get; set; }
        public int Qunatity { get; set; }
        public decimal Total { get; set; }
    }
}