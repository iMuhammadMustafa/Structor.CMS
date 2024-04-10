namespace PostsService.Features.Dtos;

public record CategoryDto : IDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

}

public record CategoryFormDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}
