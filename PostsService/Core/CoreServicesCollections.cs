using Asp.Versioning;
using System.Text.Json.Serialization;


namespace PostsService.Core;

public static class CoreServicesCollections
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration _configuration)
    {

        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        //.AddNewtonsoftJson(options =>
        //{
        //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //    options.SerializerSettings.Converters.Add(new StringEnumConverter());

        //});

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
