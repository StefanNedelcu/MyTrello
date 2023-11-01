namespace MyTrello.Dto;

public record TaskListDto
{
    public Guid? TaskListId { get; init; }
    public string Name { get; init; } = string.Empty;

    public IEnumerable<TaskDto> Tasks { get; init; } = new List<TaskDto>();
}