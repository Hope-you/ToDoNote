using TodoNote.Api.Context;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace TodoNote.Api.Service
{
    public interface IToDoService: IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllAsync(ToDoQueryParameter toDoQueryParameter);
    }
}
