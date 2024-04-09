namespace PostsService.Dtos;

public record CategoryDto : IDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public IEnumerable<PostDto>? Posts { get; set; }
}
