using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options): base(options)
        {}

        public DbSet<TodoTask> TodoTasks { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoTask>().HasData(
                new TodoTask { Id = 1, Title = "college project", description = "d1", status = "doing" },
                new TodoTask { Id = 2, Title = "company project", description = "d2", status = "doing" },
                new TodoTask { Id = 3, Title = "personal project", description = "d3", status = "completed" }
                );
        }
    }
}
