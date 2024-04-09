using Asp.Versioning;

namespace PostsService.Core;

public static class CoreServicesCollections
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration _configuration)
    {

        services.AddControllers();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer((options) =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;

    }
}
