﻿using Microsoft.EntityFrameworkCore;
using PostsService.Infrastructure.DatabaseContext;

namespace PostsService.Infrastructure
{
    public static class InfrastructureServicesCollection
    {
        private const string CONNECTION_STRING_NAME = "PostgresConnection";
        public static IServiceCollection AddInfrastructureServicesCollection(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString(CONNECTION_STRING_NAME))
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }


        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString(CONNECTION_STRING_NAME))
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }
    }
}