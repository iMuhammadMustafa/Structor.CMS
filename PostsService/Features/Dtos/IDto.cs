namespace PostsService.Features.Dtos;

public record IDto
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
