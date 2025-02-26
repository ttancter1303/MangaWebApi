using MangaWeb.Domain.Abstractions.InfrastructureServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MangaWeb.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddServicesInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        services.AddTransient<IFileService, FileService>();
        //.AddSingleton<ICacheService, CacheService>()
    }

    //public static void AddRedisInfrastructure(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddStackExchangeRedisCache(redisOptions =>
    //    {
    //        var connectionString = configuration["ConnectionStrings:Redis"];
    //        redisOptions.Configuration = connectionString;
    //    });
    //}
}