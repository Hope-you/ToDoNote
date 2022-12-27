using Microsoft.EntityFrameworkCore;
using MyToDo.Api;
using TodoNote.Api.Context;

namespace TodoNote.Api.Repository
{
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(ToDoNoteContext dbContext) : base(dbContext)
        {
        }
    }
}
