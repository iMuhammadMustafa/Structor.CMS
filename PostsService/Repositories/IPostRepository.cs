using PostsService.Entities;
using PostsService.Infrastructure.Repositories;

namespace PostsService.Repositories;

public interface IPostRepository : IRepository<Post>
{
}
