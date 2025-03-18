using MangaWeb.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MangaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MangaWeb.Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddSqlServerPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(ApplicationDbContext).Assembly.GetName().Name;
        var defaultConnection = configuration.GetConnectionString("DefaultConnection");
        var secondaryConnection = configuration.GetConnectionString("SecondaryConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // Sử dụng DefaultConnection nếu có, nếu không thì fallback sang SecondaryConnection
            var connectionString = !string.IsNullOrEmpty(defaultConnection) ? defaultConnection : secondaryConnection;
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly(assembly));
        });

        services.AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.AllowedForNewUsers = true; // Default true
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // Default 5
            options.Lockout.MaxFailedAccessAttempts = 3; // Default 5
            options.Lockout.AllowedForNewUsers = true; // Default true
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // Default 5
            options.Lockout.MaxFailedAccessAttempts = 3; // Default 5
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.Lockout.AllowedForNewUsers = true;
        });
    }

    public static void AddRepositoryUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork));
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
    }

}
