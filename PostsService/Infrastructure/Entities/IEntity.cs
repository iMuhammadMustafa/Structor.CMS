namespace PostsService.Infrastructure.Entities;

public abstract class IEntity
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public string CreatedBy { get; set; } = "SYSTEM";
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    public string? UpdatedBy { get; set; } = "SYSTEM";

}
