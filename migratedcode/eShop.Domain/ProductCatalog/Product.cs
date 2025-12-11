using eShop.Domain.Exceptions;
using System;

namespace eShop.Domain.ProductCatalog
{
    public class Product : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public ProductBrand Brand { get; set; }
        public int ProductBrandId { get; set; }
        public int StockAvailable { get; set; }
        public int ReOrderThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public bool IsOnReorder { get; set; }

        public Product()
        {
        }

        public int AddStock(int quantity)
        {
            int currentStock = this.StockAvailable;
            if ((this.StockAvailable + quantity) >= MaxStockThreshold)
            {
                this.StockAvailable += this.MaxStockThreshold - this.StockAvailable;
            }
            else
            {
                this.StockAvailable += quantity;
            }

            this.IsOnReorder = false;
            return this.StockAvailable - currentStock;
        }

        public int RemoveStock(int quantity)
        {
            if (this.StockAvailable == 0)
            {
                throw new ProductDomainException($"Out of stock,Product item {Name} is sold out");
            }

            int stockToBeRemoved = Math.Min(quantity, this.StockAvailable);
            this.StockAvailable -= stockToBeRemoved;
            return stockToBeRemoved;
        }
    }
}