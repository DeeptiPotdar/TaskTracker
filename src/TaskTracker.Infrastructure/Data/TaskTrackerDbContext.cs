using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data;

public class TaskTrackerDbContext : DbContext
{
    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
        : base(options)
    {

    }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<TaskItem>()
            .HasKey(t => t.TaskId);

        modelBuilder.Entity<TaskItem>()
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);    
    }


}
