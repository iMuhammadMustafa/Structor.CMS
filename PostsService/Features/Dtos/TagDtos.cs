namespace PostsService.Features.Dtos;


public record TagFormDto
{
    public required string Name { get; set; }
}

public record TagDto : IDto
{
    public required string Name { get; set; }
}
