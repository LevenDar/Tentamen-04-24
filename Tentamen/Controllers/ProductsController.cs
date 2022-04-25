using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tentamen.Models;
using Tentamen.Services;

namespace Tentamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public ProductsController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequest request)
        {
            var product = await _dataAccess.CreateProductAsync(request);
            if (product != null)    
                return new OkObjectResult(product);

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _dataAccess.GetAllProductAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _dataAccess.GetProductAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductRequest request)
        {
            var item = await _dataAccess.UpdateProductAsync(id, request);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduct(int id)
            {           
            if (await _dataAccess.DeleteProductAsync(id) != null)
                 return new OkResult();

                return new BadRequestResult();
            }
    }
}

