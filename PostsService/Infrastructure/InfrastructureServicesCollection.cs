using Microsoft.EntityFrameworkCore;
using PostsService.Features.BackgroundServices;
using PostsService.Infrastructure.DatabaseContext;

namespace PostsService.Infrastructure;

public static class InfrastructureServicesCollection
{
    private const string CONNECTION_STRING_NAME = "DefaultConnection";
    private const string REDIS_CONNECTION_STRING_NAME = "Redis:ConnectionString";
    public static IServiceCollection AddInfrastructureServicesCollection(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddAppDbContext(_configuration);

        services.AddAutoMapper(typeof(InfrastructureServicesCollection));


        services.AddHostedService<UpdateFrequentPosts>();

        services.AddStackExchangeRedisCache(options => options.Configuration = _configuration[REDIS_CONNECTION_STRING_NAME]);

        return services;
    }


    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        return services;
    }
}
