namespace PostsService.Core
{
    public static class CoreServicesCollections
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;

        }
    }
}
