using PostsService.Features.Entities;
using PostsService.Infrastructure.DatabaseContext;
using PostsService.Infrastructure.Repositories;
namespace PostsService.Features.Repositories;

public class TagRepository : Repository<Tag, AppDbContext>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }
}
