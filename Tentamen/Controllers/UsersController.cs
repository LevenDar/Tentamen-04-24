using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tentamen.Models;
using Tentamen.Services;

namespace Tentamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public UsersController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRequest request)
        {
            var user = await _dataAccess.CreateUserAsync(request);
            if (user != null)
                return new OkObjectResult(user);

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _dataAccess.GetAllUserAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _dataAccess.GetUserAsync(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRequest request)
        {
            var item = await _dataAccess.UpdateUserAsync(id, request);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _dataAccess.DeleteUserAsync(id) != null)
                return new OkResult();

            return new BadRequestResult();
        }
    }
}
