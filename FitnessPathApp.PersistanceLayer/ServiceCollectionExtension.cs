using FitnessPathApp.DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.PersistanceLayer
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigurePersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add database
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                o.EnableSensitiveDataLogging();
                o.EnableDetailedErrors();
            });

            // Configure context for DI
            services.AddTransient<ApplicationDbContext>();
        }
    }
}
