using Microsoft.EntityFrameworkCore;
using todo_domain_entities;

namespace todo_application.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<TodoList> Todos { get; set; }
    }
}
