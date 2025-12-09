using StackExchange.Redis;
using CacheProviders.Redis;
using CacheProviders.Memory;
using CacheProviders.Models;
using CacheProviders.Abstractions;
using Microsoft.Extensions.Options;
using CacheProviders.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CacheProviders;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCaching(
        this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Bind settings
        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

        // 2. Register Memory cache
        services.AddMemoryCache();
        services.AddSingleton<MemoryCacheProvider>();

        // 3. Register Redis (optional depending on settings)
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<CacheSettings>>().Value;

            return ConnectionMultiplexer.Connect(settings.RedisConnectionString);
        });
        services.AddSingleton<RedisCacheProvider>();

        // 4. Register factory
        services.AddSingleton<ICacheFactory, CacheFactory>();

        return services;
    }
}