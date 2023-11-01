namespace MyTrello.Data.Entities;

public partial class TaskList
{
    public TaskList()
    {
        Tasks = new HashSet<Task>();
    }

    public Guid TaskListId { get; set; }
    public Guid BoardId { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual Board Board { get; set; } = new();
    public virtual ICollection<Task> Tasks { get; set; }
}