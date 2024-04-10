using PostsService.Infrastructure.Entities;

namespace PostsService.Features.Entities;

public class Category : IEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }


    public IEnumerable<Post> Posts { get; set; } = new List<Post>();
}
