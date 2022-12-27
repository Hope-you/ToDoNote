using Microsoft.EntityFrameworkCore;
using MyToDo.Api;
using TodoNote.Api.Context;

namespace TodoNote.Api.Repository
{
    public class ToDoRepository : Repository<ToDo>, IRepository<ToDo>
    {
        public ToDoRepository(ToDoNoteContext dbContext) : base(dbContext)
        {
        }
    }
}
