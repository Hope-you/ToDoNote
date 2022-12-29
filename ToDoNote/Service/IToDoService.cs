using MyToDo.Shared.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Shared;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace ToDoNote.Service
{
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponse<PagedList<ToDoDto>>> GetFilterAll(ToDoQueryParameter toDoQueryParameter);
    }
}
