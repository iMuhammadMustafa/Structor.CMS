using PostsService.Features.CachedServices;
using PostsService.Features.Repositories;
using PostsService.Features.Services;

namespace PostsService.Features;

public static class ServicesCollectionExtensions
{

    public static IServiceCollection AddRegisteredServices(this IServiceCollection services, IConfiguration _configuration)
    {
        services.RegisterRepositories(_configuration);
        services.RegisterService(_configuration);

        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration _configuration)
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

        services.AddScoped<ICachedFrequentPosts, CachedFrequentPosts>();

        return services;
    }
}
