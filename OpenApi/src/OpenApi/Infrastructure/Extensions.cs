using OpenApi.Core.Interfaces;
using OpenApi.Core.Interfaces.Clients;
using OpenApi.Infrastructure.Clients;
using OpenApi.Infrastructure.Data;
using OpenApi.Infrastructure.Hashing;

namespace OpenApi.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var basePath = configuration.GetValue<string>("UsersService:BasePath")!;

        services.AddHttpClient<IUsersServiceClient, UsersServiceClient>(c => c.BaseAddress = new Uri(basePath));
        services.AddHttpClient<IProductsServiceClient, ProductsServiceClient>(c => c.BaseAddress = new Uri(basePath));
        services.AddHttpClient<IOrdersServiceClient, OrdersServiceClient>(c => c.BaseAddress = new Uri(basePath));
        services.AddHttpClient<ITransactionsServiceClient, TransactionsServiceClient>(c => c.BaseAddress = new Uri(basePath));
        services.AddHttpClient<IAuditEventsServiceClient, AuditEventsServiceClient>(c => c.BaseAddress = new Uri(basePath));
        services.AddHttpClient<IDevicesServiceClient, DevicesServiceClient>(c => c.BaseAddress = new Uri(basePath));

        services.Configure<ConfOptions>(configuration.GetRequiredSection("Configuration"));
        services.Configure<HashOptions>(configuration.GetSection("Hash"));
        services.AddSingleton<IHashService, Sha256HashService>();
        services.AddDatabase(configuration);
        return services;
    }
}