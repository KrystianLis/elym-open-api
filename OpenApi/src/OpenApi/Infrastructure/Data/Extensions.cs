using Microsoft.EntityFrameworkCore;
using OpenApi.Core.Interfaces.Repositories;
using OpenApi.Infrastructure.Repositories;

namespace OpenApi.Infrastructure.Data;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Database");
        var databaseOptions = new DatabaseOptions();
        section.Bind(databaseOptions);
        services.AddDbContext<ApplicationDbContext>(options
            => options.UseNpgsql(databaseOptions.ConnectionString));
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}