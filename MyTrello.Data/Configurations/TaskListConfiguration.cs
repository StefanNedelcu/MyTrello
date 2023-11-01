using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTrello.Data.Entities;

namespace MyTrello.Data.Configurations;

public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
{
    public void Configure(EntityTypeBuilder<TaskList> entity)
    {
        entity.ToTable("TaskLists");

        entity.HasKey(e => e.TaskListId)
            .HasName("PRIMARY");

        entity.HasIndex(e => new { e.BoardId, e.Name }, "Unique_TaskList_in_Board")
            .IsUnique();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        entity.HasIndex(e => e.BoardId, "TaskList_to_Board_FK_idx");

        entity.Property(e => e.BoardId)
            .IsRequired();

        entity.HasOne(taskList => taskList.Board)
            .WithMany(board => board.TaskLists)
            .HasForeignKey(taskList => taskList.BoardId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("TaskList_to_Board_FK");
    }
}
