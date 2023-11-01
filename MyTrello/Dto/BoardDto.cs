namespace MyTrello.Dto;

public record BoardDto
{
    public Guid? BoardId { get; init; }
    public string Name { get; init; } = string.Empty;
}