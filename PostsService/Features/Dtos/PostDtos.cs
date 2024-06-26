﻿namespace PostsService.Features.Dtos;


public record PostFormDto()
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required int AuthorId { get; set; }
    public bool IsPublished { get; set; } = false;
    public int? CategoryId { get; set; }
    public List<TagFormDto>? Tags { get; set; }
}

public record PostFormUpdateDto()
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required int AuthorId { get; set; }
    public bool IsPublished { get; set; } = false;
    public int? CategoryId { get; set; }
}


public record PostDto() : IDto
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

    public int? CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
    public List<TagDto>? Tags { get; set; } = new List<TagDto>();

}
