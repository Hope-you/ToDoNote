using ToDoNote.Shared.Dtos;

namespace TodoNote.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string Account, string password);
        Task<ApiResponse> Register(UserDto user);
    }
}
