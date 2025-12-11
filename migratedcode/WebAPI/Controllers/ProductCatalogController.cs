using eShop.Domain.ProductCatalog;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eShop.WebApi.Controllers
{
[Route("api/[controller]")]
[ApiController]
    public class ProductCatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private static ILog log = LogManager.GetLogger(typeof(ProductCatalogController));
        private readonly AppSettings _appSettings;
        public ProductCatalogController(IProductRepository productRepository, AppSettings appSettings)
        {
            _productRepository = productRepository;
            _appSettings = appSettings;
        }

        [HttpGet]
        [Route("items")]
        public HttpResponseMessage GetAll()
        {
            log.Info($"ProductCatalogController -> GetAll() Call Initiated");
            var productItems = _productRepository.FindAll();
            if (productItems?.Count() > 0)
            {
                return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, productItems);
            }

            return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent);
        }

        [HttpGet]
        [Route("items/{Id:int}")]
        public HttpResponseMessage FindById(int id)
        {
            log.Info($"ProductCatalogController -> FindById({id}) Call Initiated");
            var item = _productRepository.FindBy(id);
            if (item != null)
            {
                return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, _productRepository.FindBy(id));
            }

            return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
        }

        [HttpGet]
        [Route("items/startswith/{name:minlength(1)}")]
        public HttpResponseMessage ListItemsWithName(string name, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            log.Info($"ProductCatalogController -> ListItemsWithName({name}, {pageSize}, {pageIndex}) Call Initiated");
            var productItems = _productRepository.SearchByName(name, pageSize, pageIndex);
            return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, productItems);
        }

        [Route("items")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateProductAsync([FromBody] Product product)
        {
            var searchProduct = await _productRepository.FindByIdAsync(product.Id);
            if (searchProduct == null)
            {
                return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Product not found");
            }

            var oldPrice = searchProduct.Price;
            var raisePriceChangeIntegrationEvent = oldPrice != product.Price;
            searchProduct = product;
            _productRepository.Save(searchProduct);
            return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status201Created, product.Id);
        }

        [HttpGet]
        [Route("items/type/{productTypeId:int}/brand/{productBrandId?}")]
        public HttpResponseMessage ListItemsWithTypeAndBrand(int productTypeId, [FromQuery] int? productBrandId = null, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            log.Info($"ProductCatalogController -> ListItemsWithTypeAndBrand({productTypeId}, {productBrandId}, {pageSize}, {pageIndex}) Call Initiated");
            var productItems = _productRepository.SearchByTypeAndBrand(productTypeId, productBrandId, pageSize, pageIndex);
            return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, productItems);
        }

        [HttpGet]
        [Route("productBrands")]
        public async Task<HttpResponseMessage> GetProductBrandsAsync()
        {
            log.Info($"ProductCatalogController -> GetProductBrandsAsync() Call Initiated");
            var productBrands = await _productRepository.GetProductBrandsAsync();
            if (productBrands?.Count() > 0)
            {
                return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, productBrands);
            }

            return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent);
        }

        [HttpGet]
        [Route("productTypes")]
        public async Task<HttpResponseMessage> GetProductTypesAsync()
        {
            log.Info($"ProductCatalogController -> GetProductTypesAsync() Call Initiated");
            var productTypes = await _productRepository.GetProductTypesAsync();
            if (productTypes?.Count() > 0)
            {
                return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, productTypes);
            }

            return Request.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent);
        }
    }
}