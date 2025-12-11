using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Web.ViewModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
        public int StockAvailable { get; set; }
        public int ReOrderThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public bool IsOnReorder { get; set; }
    }
}