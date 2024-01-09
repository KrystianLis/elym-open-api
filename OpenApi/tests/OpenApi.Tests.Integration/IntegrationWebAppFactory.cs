using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenApi.Core.Interfaces.Clients;
using OpenApi.Infrastructure;
using OpenApi.Infrastructure.Clients;
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

    private readonly UsersServiceApiServer _usersServiceApiServer = new();

    public async Task InitializeAsync()
    {
        _usersServiceApiServer.Start();
        _usersServiceApiServer.SetupServer();
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        _usersServiceApiServer.Dispose();
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
            
            services.AddHttpClient<IUsersServiceClient, UsersServiceClient>(client =>
            {
                client.BaseAddress = new Uri(_usersServiceApiServer.Url);
            });
            
            services.Configure<ConfOptions>(opts =>
            {
                opts.LastLimit = 5;
            });
        });
    }
}