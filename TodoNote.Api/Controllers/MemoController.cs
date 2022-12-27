using Microsoft.AspNetCore.Mvc;
using TodoNote.Api.Service;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace TodoNote.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService service;

        public MemoController(IMemoService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Add(MemoDto model) => await service.AddAsync(model);

        [HttpGet]

        public async Task<ApiResponse> Get(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery]QueryParameter parameter) => await service.GetAllAsync(parameter);
        [HttpPost]
        public async Task<ApiResponse> UpDate([FromBody] MemoDto model) => await service.UpdateAsync(model);

        [HttpDelete]

        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

    }
}
