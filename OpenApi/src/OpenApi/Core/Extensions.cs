using OpenApi.Core.Time;

namespace OpenApi.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IClock, UtcClock>();
        return services;
    }
}