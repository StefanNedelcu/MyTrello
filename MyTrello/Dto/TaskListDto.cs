namespace MyTrello.Dto;

public record TaskListDto
{
    public Guid BoardId { get; init; }
    public Guid TaskListId { get; init; }
    public string Name { get; init; } = string.Empty;
}