using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace OpenApi.Tests.Integration;

public abstract class BaseIntegrationTests : IClassFixture<IntegrationWebAppFactory>
{
    private readonly IServiceScope _scope;
    
    public BaseIntegrationTests(IntegrationWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
    }
    
}