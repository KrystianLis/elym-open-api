using OpenApi.Application.Services;

namespace OpenApi.Application;

public static class Extension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDataAggregatorService, DataAggregatorAggregatorService>();
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<ITransactionsService, TransactionsService>();
        services.AddScoped<IAuditEventsService, AuditEventsService>();
        services.AddScoped<IDevicesService, DevicesService>();
        return services;
    }
}