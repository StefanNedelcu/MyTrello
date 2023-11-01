namespace MyTrello.Data.Entities;

public partial class Board
{
    public Board()
    {
        TaskLists = new HashSet<TaskList>();
    }

    public Guid BoardId { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<TaskList> TaskLists { get; set; }
}