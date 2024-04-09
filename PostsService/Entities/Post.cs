using PostsService.Dtos;
using PostsService.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsService.Entities;

public class Post : IEntity
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required string Author { get; set; }
    public required int AuthorId { get; set; }
    public int Rating { get; set; }
    public int CommentCount { get; set; }
    public int SavedCount { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsPublished { get; set; } = false;

    [ForeignKey(nameof(Category))]
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();


    public PostDto MapToDto()
    {
        var newPost = new PostDto()
        {
            Id = Id,
            Guid = Guid,
            CreatedAt = CreatedAt,
            Title = Title,
            Content = Content,
            Author = Author,
            AuthorId = AuthorId,
            Rating = Rating,
            CommentCount = CommentCount,
            SavedCount = SavedCount,
            IsDeleted = IsDeleted,
            IsPublished = IsPublished,

            CategoryId = CategoryId,
            Category = Category?.MapToDto(),
            Tags = Tags.Select(tag => tag.MapToDto())
        };
        return newPost;
    }


}
