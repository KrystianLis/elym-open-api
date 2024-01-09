using Microsoft.Extensions.DependencyInjection;

namespace E2E.OpenApiTests.Endpoints;

public class BaseEndpoint
{
    private readonly ServiceProvider _serviceProvider;

    protected BaseEndpoint()
    {
        var services = new ServiceCollection();
        services.AddHttpClient();
        _serviceProvider = services.BuildServiceProvider();
    }
    
    protected HttpClient GetHttpClient()
    {
        var httpClientFactory = _serviceProvider.GetService<IHttpClientFactory>();
        return httpClientFactory!.CreateClient();
    }
}