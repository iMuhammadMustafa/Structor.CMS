using PostsService.Features.Repositories;
using PostsService.Features.Services;

namespace PostsService;

public static class ServicesCollectionExtensions
{

    public static IServiceCollection AddRegisteredServices(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddRepositories(_configuration);
        services.RegisterService(_configuration);

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();


        return services;
    }

    public static IServiceCollection RegisterService(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITagService, TagService>();


        return services;
    }
}
