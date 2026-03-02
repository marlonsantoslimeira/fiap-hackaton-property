namespace AgroSolutions.Properties.Application.DTOs;

public record FieldOutputDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid PropertyId { get; init; }
    public Guid Culture { get; init; }
    public DateTime CreatedAt { get; init; }
}
