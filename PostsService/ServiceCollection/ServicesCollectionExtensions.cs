using PostsService.Repositories;

namespace PostsService.ServiceCollection;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }
}
