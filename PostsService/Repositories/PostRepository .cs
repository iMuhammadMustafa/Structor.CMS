using PostsService.Entities;
using PostsService.Infrastructure.DatabaseContext;
using PostsService.Infrastructure.Repositories;
namespace PostsService.Repositories;

public class PostRepository : Repository<Post, AppDbContext>, IPostRepository
{
    public PostRepository(AppDbContext context) : base(context)
    {
    }
}
