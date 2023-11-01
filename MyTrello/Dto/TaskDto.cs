namespace MyTrello.Dto;

public record TaskDto
{
    public Guid? TaskId { get; init; }
    public string Info { get; init; } = string.Empty;
    public string? Description { get; init; }
}