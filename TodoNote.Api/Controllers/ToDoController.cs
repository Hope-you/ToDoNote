using Microsoft.AspNetCore.Mvc;
using MyToDo.Api;
using TodoNote.Api.Context;
using TodoNote.Api.Repository;
using TodoNote.Api.Service;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace TodoNote.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService service;

        public ToDoController(IToDoService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Add(ToDoDto model) => await service.AddAsync(model);

        [HttpGet]

        public async Task<ApiResponse> Get(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] ToDoQueryParameter parameter) => await service.GetAllAsync(parameter);
        [HttpPost]
        public async Task<ApiResponse> UpDate([FromBody] ToDoDto toDo) => await service.UpdateAsync(toDo);

        [HttpDelete]

        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

    }
}
