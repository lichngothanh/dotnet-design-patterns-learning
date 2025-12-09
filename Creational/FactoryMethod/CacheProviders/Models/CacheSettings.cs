using CacheProviders.Abstractions;

namespace CacheProviders.Models;

public class CacheSettings
{
    public CacheProviderType Provider { get; set; } = CacheProviderType.Memory;
    public string RedisConnectionString { get; set; } = string.Empty;
}