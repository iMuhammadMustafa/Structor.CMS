namespace PostsService.Infrastructure.Entities;

public abstract class IEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public string CreatedBy { get; set; } = "SYSTEM";
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public string? UpdatedBy { get; set; } = "SYSTEM";

}
