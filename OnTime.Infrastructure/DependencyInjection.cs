using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnTime.Infrastructure.Common.Persistence;

namespace OnTime.Infrastructure;

public static partial class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<OnTimeDbContext>(options => options.UseSqlite("Data Source = OnTime.sqlite", m => m.MigrationsAssembly("OnTime.Api")));

        return services;
    }
}

