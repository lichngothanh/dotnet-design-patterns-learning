namespace CacheProviders.Abstractions;

/// <summary>
/// Specifies the available cache provider types supported by the system.
/// </summary>
public enum CacheProviderType
{
    /// <summary>
    /// In-memory cache stored locally within the application process.
    /// </summary>
    Memory,

    /// <summary>
    /// Distributed cache backed by a Redis server or cluster.
    /// </summary>
    Redis,

    /// <summary>
    /// Persistent or semi-persistent cache backed by AWS DynamoDB.
    /// </summary>
    DynamoDb
}