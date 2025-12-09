using CacheProviders.Abstractions;
using CacheProviders.Memory;
using CacheProviders.Models;
using CacheProviders.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CacheProviders.Implementations;

public class CacheFactory(IServiceProvider provider, IOptions<CacheSettings> settings) : ICacheFactory
{
    private readonly CacheSettings _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    public ICacheProvider CreateProvider()
    {
        return _settings.Provider switch
        {
            CacheProviderType.Memory => provider.GetRequiredService<MemoryCacheProvider>(),
            CacheProviderType.Redis => provider.GetRequiredService<RedisCacheProvider>(),
            CacheProviderType.DynamoDb => throw new NotSupportedException("DynamoDb is not supported yet."),
            _ => throw new NotSupportedException($"Unknown cache provider: {settings.Value.Provider}")
        };
    }
}