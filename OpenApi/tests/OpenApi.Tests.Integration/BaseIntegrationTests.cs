using Microsoft.Extensions.DependencyInjection;
using OpenApi.Infrastructure.Data;
using Xunit;

namespace OpenApi.Tests.Integration;

public abstract class BaseIntegrationTests : IClassFixture<IntegrationWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTests(IntegrationWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}