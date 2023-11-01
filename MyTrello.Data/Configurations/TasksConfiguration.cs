using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyTrello.Data.Configurations;

public class TasksConfiguration : IEntityTypeConfiguration<Entities.Task>
{
    public void Configure(EntityTypeBuilder<Entities.Task> entity)
    {
        entity.ToTable("Tasks");

        entity.HasKey(e => e.TaskId)
            .HasName("PRIMARY");

        entity.HasIndex(e => new { e.Info, e.TaskListId }, "Unique_Task_in_TaskList")
            .IsUnique();
        entity.Property(e => e.Info)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.Description)
            .HasMaxLength(1000);

        entity.HasIndex(e => e.TaskListId, "Task_to_TaskList_FK_idx");
        entity.Property(e => e.TaskListId)
            .IsRequired();

        entity.HasOne(task => task.TaskList)
            .WithMany(taskList => taskList.Tasks)
            .HasForeignKey(task => task.TaskListId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("Task_to_TaskList_FK");
    }
}
