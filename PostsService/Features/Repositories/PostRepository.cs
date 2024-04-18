using Microsoft.EntityFrameworkCore;
using PostsService.Features.Entities;
using PostsService.Infrastructure.DatabaseContext;
using PostsService.Infrastructure.DTOs.REST;
using PostsService.Infrastructure.Extentions;
using PostsService.Infrastructure.Repositories;
namespace PostsService.Features.Repositories;

public class PostRepository : Repository<Post, AppDbContext>, IPostRepository
{

    public PostRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Post>> GetAllPaginated(Pagination pagination)
    {
        var query = DbSet.AsQueryable()
                         .Include(post => post.Tags)
                         .Include(post => post.Category);

        pagination.TotalCount = await query.CountAsync();
        var dataTask = await query.Paginate(pagination.Page, pagination.Size)
                                  .ToListAsync();

        //await Task.WhenAll(dataTask, countTask);

        //var data = dataTask.Result;
        //pagination.TotalCount = countTask.Result;
        //pagination.TotalPages = pagination.TotalCount / pagination.Size;


        return dataTask;
    }

    public async Task<IEnumerable<Post>> GetAllByCategoryId(int categoryId, Pagination pagination)
    {
        var query = DbSet.Include(p => p.Tags)
                         .Include(p => p.Category)
                         .Where(post => post.CategoryId == categoryId);

        pagination.TotalCount = await query.CountAsync();
        var data = await query.Paginate(pagination.Page, pagination.Size)
                            .ToListAsync();

        return data;
    }

    public async Task<IEnumerable<Post>> GetAllByTagId(int tagId, Pagination pagination)
    {
        var query = DbSet.Include(p => p.Tags)
                         .Include(p => p.Category)
                         .Where(post => post.Tags.Any(tag => tag.Id == tagId));


        pagination.TotalCount = await query.CountAsync();
        var data = await query.Paginate(pagination.Page, pagination.Size)
                                  .ToListAsync();

        return data;
    }
}
