using Microsoft.EntityFrameworkCore;
using MyToDo.Api;
using TodoNote.Api.Context;

namespace TodoNote.Api.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(ToDoNoteContext dbContext) : base(dbContext)
        {
        }
    }
}
