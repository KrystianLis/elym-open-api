using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenApi.Infrastructure.Data;
using Testcontainers.PostgreSql;
using Xunit;

namespace OpenApi.Tests.Integration;

public class IntegrationWebAppFactory : WebApplicationFactory<IApiMaker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("testDb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(s
                => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }
}