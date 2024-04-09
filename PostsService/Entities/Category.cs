using PostsService.Dtos;
using PostsService.Infrastructure.Entities;

namespace PostsService.Entities;

public class Category : IEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }


    public IEnumerable<Post> Posts { get; set; } = new List<Post>();


    public CategoryDto MapToDto()
    {
        CategoryDto categoryDto = new CategoryDto()
        {
            Id = Id,
            Guid = Guid,
            CreatedAt = CreatedAt,
            Name = Name,
            Description = Description,
            Posts = Posts.Select(post => post.MapToDto()).ToList()

        };

        return categoryDto;
    }
}
