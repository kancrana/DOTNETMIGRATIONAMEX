using eShop.Domain.ProductCatalog;
using eShop.Domain.ProductCatalog.ViewModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repository.EntityFramework.ProductCatalog
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDataContext _context;
        public ProductRepository(ProductDataContext context)
        {
            _context = context;
        }

        public void Add(Product entity)
        {
            try
            {
                _context.Product.Add(entity);
            }
            catch (Exception ex)
            {
            }
        }

        public void BulkInsert(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> FindAll()
        {
            try
            {
                return _context.Product.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Product>> FindAllAsync()
        {
            try
            {
                return await _context.Product.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Product FindBy(int id)
        {
            try
            {
                return _context.Product.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Product.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductBrand>> GetProductBrandsAsync()
        {
            try
            {
                return await _context.Brand.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            try
            {
                return await _context.ProductType.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Save(Product entity)
        {
            throw new NotImplementedException();
        }

        public PaginatedViewModel<Product> SearchByName(string name, int pageSize, int pageIndex)
        {
            try
            {
                var totalItems = this._context.Product.Where(c => c.Name.StartsWith(name)).LongCount();
                var itemsOnPage = this._context.Product.Where(c => c.Name.StartsWith(name)).OrderBy(c => c.Name).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, itemsOnPage);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PaginatedViewModel<Product>> SearchByNameAsync(string name, int pageSize, int pageIndex)
        {
            try
            {
                var totalItems = await this._context.Product.Where(c => c.Name.StartsWith(name)).LongCountAsync();
                var itemsOnPage = await this._context.Product.Where(c => c.Name.StartsWith(name)).OrderBy(c => c.Name).Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, itemsOnPage);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PaginatedViewModel<Product> SearchByTypeAndBrand(int typeId, int? brandId, int pageSize, int pageIndex)
        {
            try
            {
                IQueryable<Product> _product = this._context.Product.Where(x => x.ProductTypeId == typeId && (brandId == null || x.ProductBrandId == brandId));
                var totalItems = _product.LongCount();
                var itemsOnPage = _product.OrderBy(c => c.Name).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, itemsOnPage);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PaginatedViewModel<Product>> SearchByTypeAndBrandAsync(int typeId, int? brandId, int pageSize, int pageIndex)
        {
            try
            {
                IQueryable<Product> _product = this._context.Product.Where(x => x.ProductTypeId == typeId && (brandId == null || x.ProductBrandId == brandId));
                var totalItems = await _product.LongCountAsync();
                var itemsOnPage = await _product.OrderBy(c => c.Name).Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, itemsOnPage);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}