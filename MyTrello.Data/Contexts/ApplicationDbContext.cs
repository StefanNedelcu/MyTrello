using Microsoft.EntityFrameworkCore;
using MyTrello.Data.Entities;
using System.Reflection;

namespace MyTrello.Data.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public ApplicationDbContext()
    {
    }

    public virtual DbSet<Board> Boards { get; set; }
    public virtual DbSet<TaskList> TaskLists { get; set; }
    public virtual DbSet<Entities.Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
