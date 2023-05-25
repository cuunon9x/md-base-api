using md.Services.IRepositories;
using md.Services.Util;
using md.Services.ViewModels.ConsumerViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [LoggingFilter]
    [ApiController]
    [Route("api/consumer")]
    public class ConsumerController : Controller
    {
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerController(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllProduct(int? pageNumber = null, int? pageSize = null)
        {
            var consumers = await _consumerRepository.GetAllConsumerAsync(pageNumber ?? CommonConstants.Paging.DefaultPageNumber, pageSize ?? CommonConstants.Paging.DefaultPageSize);
            return Ok(consumers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchConsumersByName(string textSearch, int? pageNumber, int? pageSize = null)
        {
            var consumer = await _consumerRepository.SearchConsumersByName(textSearch, pageNumber ?? CommonConstants.Paging.DefaultPageNumber, pageSize ?? CommonConstants.Paging.DefaultPageSize);
            if (consumer == null)
                return BadRequest("Not Found!");
            return Ok(consumer);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateConsumer(ConsumerRequest request)
        {
            var consumerId = await _consumerRepository.CreateAsync(request);
            var consumer = await _consumerRepository.GetConsumerByIdAsync(consumerId);
            if (consumer == null)
                return BadRequest();
            return CreatedAtAction(nameof(CreateConsumer), new { id = consumerId }, consumer);
        }
    }
}
