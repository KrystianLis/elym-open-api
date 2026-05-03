using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenApi.Application;
using OpenApi.Application.Services;
using OpenApi.Core;
using OpenApi.Infrastructure;
using OpenApi.Infrastructure.Data;
using OpenApi.Infrastructure.Errors;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthenticationCore()
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddErrorHandler();

var app = builder.Build();

app.UseErrorHandler();

app.MapGet("/latest-users", async ([FromServices] IDataAggregatorService dataService, CancellationToken token) =>
{
        var entries = await dataService.GetLatestUsersAsync(token);
        return Results.Ok(entries);
}).WithName("Get latest users");

app.MapGet("/products", async ([FromServices] IProductsService service, CancellationToken token)
    => Results.Ok(await service.GetProductsAsync(token))).WithName("Get products");

app.MapGet("/orders", async ([FromServices] IOrdersService service, CancellationToken token) =>
{
    var order = await service.GetOrderAsync(token);
    return order is null ? Results.NotFound() : Results.Ok(order);
}).WithName("Get order");

app.MapGet("/transactions", async ([FromServices] ITransactionsService service, CancellationToken token)
    => Results.Ok(await service.GetTransactionsAsync(token))).WithName("Get transactions");

app.MapGet("/audit-events", async ([FromServices] IAuditEventsService service, CancellationToken token)
    => Results.Ok(await service.GetAuditEventsAsync(token))).WithName("Get audit events");

app.MapGet("/devices", async ([FromServices] IDevicesService service, CancellationToken token)
    => Results.Ok(await service.GetDevicesAsync(token))).WithName("Get devices");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();