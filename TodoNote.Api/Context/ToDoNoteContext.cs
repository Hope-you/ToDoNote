using Microsoft.EntityFrameworkCore;

namespace TodoNote.Api.Context
{
    public class ToDoNoteContext : DbContext
    {

        public ToDoNoteContext(DbContextOptions<ToDoNoteContext> options) : base(options)
        {
        }


        public DbSet<ToDo> ToDo { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Memo> Memo { get; set; }
    }
}
