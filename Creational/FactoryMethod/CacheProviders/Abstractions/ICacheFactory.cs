namespace CacheProviders.Abstractions;

/// <summary>
/// Defines a factory responsible for creating cache provider instances.
/// </summary>
/// <remarks>
/// This abstraction allows selecting different cache providers at runtime,
/// such as Redis, DynamoDB, or in-memory implementations.
/// </remarks>
public interface ICacheFactory
{
    /// <summary>
    /// Creates and returns a cache provider instance.
    /// </summary>
    /// <returns>An <see cref="ICacheProvider"/> implementation.</returns>
    ICacheProvider CreateProvider();
}