using PostsService.Repositories;

namespace PostsService;

public static class ServicesCollectionExtensions
{

    public static IServiceCollection AddRegisteredServices(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddRepositories(_configuration);

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }
}
