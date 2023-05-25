using md.Services.IRepositories;
using md.Services.Util;
using md.Services.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [LoggingFilter]
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllProduct(int? pageNumber = null, int? pageSize = null)
        {
            var products = await _productRepository.GetAllProductAsync(pageNumber ?? CommonConstants.Paging.DefaultPageNumber, pageSize ?? CommonConstants.Paging.DefaultPageSize);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(Guid productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
                return BadRequest("Not Found!");
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProductsByName(string textSearch, int? pageNumber, int? pageSize = null)
        {
            var products = await _productRepository.SearchProductsByName(textSearch, pageNumber ?? CommonConstants.Paging.DefaultPageNumber, pageSize ?? CommonConstants.Paging.DefaultPageSize);
            if (products == null)
                return BadRequest("Not Found!");
            return Ok(products);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateProducts(ProductRequest request)
        {
            var productId = await _productRepository.CreateAsync(request);
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
                return BadRequest();
            return CreatedAtAction(nameof(CreateProducts), new { id = productId }, product);
        }

    }
}
