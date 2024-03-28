using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OnTime.Application.Common.Interfaces;
using OnTime.Infrastructure.Appointments.Persistence;
using OnTime.Infrastructure.Common.Persistence;
using OnTime.Infrastructure.Token;
using OnTime.Infrastructure.UserProvider;
using OnTime.Infrastructure.Users.Persistence;

namespace OnTime.Infrastructure;

public static partial class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddAuthorization()
            .AddAuthentication(configuration)
            .AddPersistence();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<OnTimeDbContext>(options => options.UseSqlite("Data Source = OnTime.sqlite", m => m.MigrationsAssembly("OnTime.Api")));

        services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}

