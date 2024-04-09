using PostsService.Infrastructure.Entities;

namespace PostsService.Entities;

public class Tag : IEntity
{
    public required string Name { get; set; }
    public IEnumerable<Post> Posts { get; set; } = new List<Post>();

    public TagDto MapToDto()
    {
        TagDto tagDto = new TagDto()
        {
            Id = Id,
            Guid = Guid,
            CreatedAt = CreatedAt,
            Name = Name,
            Posts = Posts.Select(post => post.MapToDto()).ToList()

        };

        return tagDto;
    }
}
