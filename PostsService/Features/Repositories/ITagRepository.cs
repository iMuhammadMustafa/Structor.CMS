using PostsService.Features.Entities;
using PostsService.Infrastructure.Repositories;

namespace PostsService.Features.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> FindAllByIds(List<int> ids);
    Task<IEnumerable<Tag>> FindAllByNames(List<string> names);
    Task<Tag?> FindByName(string name);

}
