using System.Text.Json;
using StackExchange.Redis;
using CacheProviders.Abstractions;

namespace CacheProviders.Redis;

public class RedisCacheProvider(IConnectionMultiplexer connection) : ICacheProvider
{
    private readonly IDatabase _db = connection.GetDatabase();

    public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        var json = JsonSerializer.Serialize(value);

        if (expirationTime is not null)
        {
            await _db.StringSetAsync(key, json, (Expiration)expirationTime);
            return;
        }

        await _db.StringSetAsync(key, json);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);

        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value.ToString());
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key); 
    }

    public async Task ClearAsync()
    {
        foreach (var endPoint in connection.GetEndPoints())
        {
            var server = connection.GetServer(endPoint);
            await server.FlushDatabaseAsync(_db.Database); 
        }
    }
}