using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tentamen.Models;
using Tentamen.Services;

namespace Tentamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public OrdersController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var order = await _dataAccess.CreateOrderAsync(request);
            if (order != null)
                return new OkObjectResult(order);

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _dataAccess.GetAllOrderAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _dataAccess.GetOrderAsync(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderRequest request)
        {
            var item = await _dataAccess.UpdateOrderAsync(id, request);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (await _dataAccess.DeleteOrderAsync(id) != null)
                return new OkResult();

            return new BadRequestResult();
        }
    }
}
