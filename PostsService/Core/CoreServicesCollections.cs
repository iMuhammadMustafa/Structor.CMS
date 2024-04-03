using PostsService.Infrastructure;

namespace PostsService.Core;

public static class CoreServicesCollections
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration _configuration)
    {

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        services.AddInfrastructureServicesCollection(_configuration);

        return services;

    }
}
