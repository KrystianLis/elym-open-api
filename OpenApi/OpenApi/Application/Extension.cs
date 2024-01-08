using OpenApi.Application.Services;

namespace OpenApi.Application;

public static class Extension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDataAggregatorService, DataAggregatorAggregatorService>();
        return services;
    }
}