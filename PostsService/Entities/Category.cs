using PostsService.Infrastructure.Entities;

namespace PostsService.Entities
{
    public class Category : IEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }


        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
