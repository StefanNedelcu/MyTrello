namespace MyTrello.Data.Entities;

public partial class Task
{
    public Guid TaskId { get; set; }
    public Guid TaskListId { get; set; }
    public string Info { get; set; } = string.Empty;
    public string? Description { get; set; }

    public virtual TaskList TaskList { get; set; } = new();
}