using Microsoft.EntityFrameworkCore;
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

        public DbSet<Todo> Todos { get; set; }
    }
}
