using PostsService.Dtos;

namespace PostsService.Entities;

public record TagDto : IDto
{
    public required string Name { get; set; }
    public IEnumerable<PostDto>? Posts { get; set; }
}
