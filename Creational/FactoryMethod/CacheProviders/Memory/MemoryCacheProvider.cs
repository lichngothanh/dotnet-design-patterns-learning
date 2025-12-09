using CacheProviders.Abstractions;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Caching.Memory;

namespace CacheProviders.Memory;

public class MemoryCacheProvider(IMemoryCache cache) : ICacheProvider
{
    private CancellationTokenSource _tokenSource = new();

    public Task SetAsync<T>(string key,
        T value,
        TimeSpan? expirationTime = null)
    {
        using var entry = cache.CreateEntry(key);
        entry.Value = value;
        entry.AbsoluteExpirationRelativeToNow = expirationTime;

        // Automatically register the global invalidation token
        entry.ExpirationTokens.Add(new CancellationChangeToken(_tokenSource.Token));

        return Task.CompletedTask;
    }

    public Task<T?> GetAsync<T>(string key)
        => Task.FromResult(cache.Get<T>(key));

    public Task RemoveAsync(string key)
    {
        cache.Remove(key);
        return Task.CompletedTask;
    }

    public Task ClearAsync()
    {
        _tokenSource.Cancel();
        _tokenSource.Dispose();

        _tokenSource = new CancellationTokenSource();

        return Task.CompletedTask;
    }
}