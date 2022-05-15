using FitnessPathApp.DomainLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace FitnessPathApp.Tests.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        public async Task<IHost> CreateHost()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHostBuilder =>
                {
                    // configure test web host which will be used to create test server
                    webHostBuilder
                        .ConfigureServices((builder, services) =>
                        {
                            var provider = services
                                .AddEntityFrameworkSqlServer()
                                .BuildServiceProvider();

                            var connectionString = "Server=DESKTOP-7B8M2IS\\SQLEXPRESS;Database=fitnessApp_test_db;Trusted_Connection=True;";

                           var optionsBuilder = services.AddDbContext<ApplicationDbContext>(options =>
                            {
                                options.UseSqlServer(connectionString)
                                .UseInternalServiceProvider(provider);
                            });

                        })
                        .UseStartup<Startup>()
                        .UseTestServer();
                });

            var host = await hostBuilder.StartAsync();

            // setup stuff
            // db migrations etc..
            // ReSharper disable once ConvertToUsingDeclaration
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                ReseedDatabase(services);
            }

            return host;
        }

        private void ReseedDatabase(IServiceProvider services)
        {
            var dbContext = services.GetService<ApplicationDbContext>();

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

    }
}
