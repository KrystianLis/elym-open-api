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