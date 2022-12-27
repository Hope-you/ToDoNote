using Microsoft.AspNetCore.Mvc;
using TodoNote.Api.Service;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace TodoNote.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly ILoginService service;

        public LoginController(ILoginService service)
        {
            this.service = service;
        }


        [HttpGet]

        public async Task<ApiResponse> Login(string account, string pwd) => await service.LoginAsync(account, pwd);

        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto model) => await service.Register(model);
    }
}
