using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostsService.Features.BackgroundServices;
using PostsService.Infrastructure.DatabaseContext;

namespace PostsService.Infrastructure;

public static class InfrastructureServicesCollection
{
    private const string CONNECTION_STRING_NAME = "DefaultConnection";
    private const string REDIS_CONNECTION_STRING_NAME = "Redis:ConnectionString";
    private const string RabbitMQ_CONNECTION_HOST = "RabbitMq:Connection:Host";
    private const string RabbitMQ_CONNECTION_USERNAME = "RabbitMq:Connection:Username";
    private const string RabbitMQ_CONNECTION_PASSWORD = "RabbitMq:Connection:Password";
    public static IServiceCollection AddInfrastructureServicesCollection(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddAppDbContext(_configuration);

        services.AddAutoMapper(typeof(InfrastructureServicesCollection));


        services.AddHostedService<UpdateFrequentPosts>();

        services.AddStackExchangeRedisCache(options => options.Configuration = _configuration[REDIS_CONNECTION_STRING_NAME]);


        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumers(typeof(Program).Assembly);

            x.UsingRabbitMq((context, config) =>
            {
                config.Host(_configuration[RabbitMQ_CONNECTION_HOST], "/", host =>
                {
                    host.Username(_configuration[RabbitMQ_CONNECTION_USERNAME]);
                    host.Password(_configuration[RabbitMQ_CONNECTION_PASSWORD]);
                });

                config.ConfigureEndpoints(context);
                config.UseRawJsonSerializer();
                config.UseRawJsonDeserializer();
            });


        });

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
