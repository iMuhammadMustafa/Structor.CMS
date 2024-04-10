using PostsService.Features.Entities;
using PostsService.Infrastructure.Repositories;

namespace PostsService.Features.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
}
