namespace CacheProviders.Abstractions;

/// <summary>
/// Defines a cache provider capable of storing and retrieving typed values.
/// </summary>
/// <remarks>
/// Implementations may use different underlying storage mechanisms 
/// such as Redis, in-memory cache, or distributed cache.
/// </remarks>
public interface ICacheProvider
{
    /// <summary>
    /// Stores a value in the cache with a specific time-to-live (TTL).
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="key">The unique key used to identify the cached value.</param>
    /// <param name="value">The value to be cached.</param>
    /// <param name="expirationTime">The expiration duration before the entry is removed.</param>
    Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null);
    
    /// <summary>
    /// Retrieves a cached value by its key.
    /// </summary>
    /// <typeparam name="T">The expected type of the cached value.</typeparam>
    /// <param name="key">The unique key used to look up the cached entry.</param>
    /// <returns>
    /// The cached value if found; otherwise, <c>null</c>.
    /// </returns>
    Task<T?> GetAsync<T>(string key);

    /// <summary>
    /// Removes a cached value associated with the specified key.
    /// </summary>
    /// <param name="key">The unique key identifying the cached entry to be removed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// Completes once the entry has been successfully removed from the cache.
    /// </returns>
    Task RemoveAsync(string key);

    /// <summary>
    /// Clears all entries from the cache.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// Completes when all cache entries have been successfully removed.
    /// </returns>
    Task ClearAsync();
}