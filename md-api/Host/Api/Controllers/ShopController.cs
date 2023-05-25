using md.Services.IRepositories;
using md.Services.Util;
using md.Services.ViewModels.ShopViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [LoggingFilter]
    [ApiController]
    [Route("api/shops")]
    public class ShopController : Controller
    {
        private readonly IShopRepository _shopRepository;

        public ShopController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllShop(int? pageNumber = null, int? pageSize = null)
        {
            var shops = await _shopRepository.GetAllShopAsync(pageNumber ?? CommonConstants.Paging.DefaultPageNumber, pageSize ?? CommonConstants.Paging.DefaultPageSize);
            return Ok(shops);
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> GetById(Guid ShopId)
        {
            var shop = await _shopRepository.GetShopByIdAsync(ShopId);
            if (shop == null)
                return BadRequest("Not Found!");
            return Ok(shop);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchShopsByName(string textSearch)
        {
            var shop = _shopRepository.SearchShopsByName(textSearch);
            if (shop == null)
                return BadRequest("Not Found!");
            return Ok(shop);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateShops(ShopRequest request)
        {
            var shopId = await _shopRepository.CreateAsync(request);
            var shop = await _shopRepository.GetShopByIdAsync(shopId);
            if (shop == null)
                return BadRequest();
            return CreatedAtAction(nameof(CreateShops), new { id = shopId }, shop);
        }
    }
}
