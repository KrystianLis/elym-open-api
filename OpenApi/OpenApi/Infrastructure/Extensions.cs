using OpenApi.Core.Interfaces.Clients;
using OpenApi.Infrastructure.Clients;
using OpenApi.Infrastructure.Data;

namespace OpenApi.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IUsersServiceClient, UsersServiceClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("UsersService:BasePath")!);
        });
        
        services.Configure<ConfOptions>(configuration.GetRequiredSection("Configuration"));
        services.AddDatabase(configuration);
        return services;
    }
}