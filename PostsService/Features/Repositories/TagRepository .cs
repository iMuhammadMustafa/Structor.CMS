using Microsoft.EntityFrameworkCore;
using PostsService.Features.Entities;
using PostsService.Infrastructure.DatabaseContext;
using PostsService.Infrastructure.Extentions;
using PostsService.Infrastructure.Repositories;
namespace PostsService.Features.Repositories;

public class TagRepository : Repository<Tag, AppDbContext>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Tag>> FindAllByIds(List<int> ids)
    {
        var data = await FindAllWhere(tag => ids.Contains(tag.Id));


        return data;
    }
    public async Task<IEnumerable<Tag>> FindAllByNames(List<string> names) => await FindAllWhere(tag => names.Contains(tag.Name));


    //TODO: Configure Database to be case insenstive 
    public async Task<Tag?> FindByName(string name) => await DbSet.FirstOrDefaultAsync(tag => tag.Name.Equals(name.ToTitleCase()));
}
