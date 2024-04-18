using PostsService.Features.Entities;
using PostsService.Infrastructure.DatabaseContext;
using PostsService.Infrastructure.Repositories;
namespace PostsService.Features.Repositories;

public class CategoryRepository : Repository<Category, AppDbContext>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
