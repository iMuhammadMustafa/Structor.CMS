using PostsService.Infrastructure.Entities;

namespace PostsService.Entities;

public class Tag : IEntity
{
    public required string Name { get; set; }


    public List<Post> Posts { get; set; } = new List<Post>();
}
