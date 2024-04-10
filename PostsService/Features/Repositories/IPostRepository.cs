using PostsService.Features.Entities;
using PostsService.Infrastructure.DTOs.REST;
using PostsService.Infrastructure.Repositories;

namespace PostsService.Features.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetAllPaginated(Pagination pagination);
    Task<IEnumerable<Post>> GetAllByCategoryId(int categoryId, Pagination pagination);
    Task<IEnumerable<Post>> GetAllByTagId(int tagId, Pagination pagination);
}
