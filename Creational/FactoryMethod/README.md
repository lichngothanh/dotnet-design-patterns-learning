# Factory Method Pattern - Cache Provider Implementation
Document: https://refactoring.guru/design-patterns/factory-method
This project demonstrates the **Factory Method** design pattern through a flexible caching system that supports multiple
cache providers (Memory, Redis, and DynamoDB).

## Overview

The Factory Method pattern defines an interface for creating objects but lets subclasses decide which class to
instantiate. In this implementation, the `ICacheFactory` interface allows runtime selection of different cache providers
based on configuration.

## Project Structure

```
CacheProviders/
├── Abstractions/
│   ├── ICacheProvider.cs        # Common interface for all cache providers
│   ├── ICacheFactory.cs         # Factory interface
│   └── CacheProviderType.cs     # Enum defining provider types
├── Implementations/
│   └── CacheFactory.cs          # Concrete factory implementation
├── Memory/
│   └── MemoryCacheProvider.cs   # In-memory cache implementation
├── Redis/
│   └── RedisCacheProvider.cs    # Redis cache implementation
├── Models/
│   └── CacheSettings.cs         # Configuration model
└── ServiceCollectionExtensions.cs
```

## Key Components

### 1. Product Interface (`ICacheProvider`)

Defines the contract that all cache providers must implement:

- `SetAsync<T>()` - Store values with optional TTL
- `GetAsync<T>()` - Retrieve cached values
- `RemoveAsync()` - Remove specific cache entries
- `ClearAsync()` - Clear all cache entries

### 2. Concrete Products

#### MemoryCacheProvider

- Uses `IMemoryCache` for local in-process caching
- Supports global cache invalidation via `CancellationTokenSource`
- Best for single-instance applications

#### RedisCacheProvider

- Uses `StackExchange.Redis` for distributed caching
- Serializes values to JSON
- Best for multi-instance/distributed applications

## Usage

### Configuration

Add to `appsettings.json`:

```json
{
  "CacheSettings": {
    "Provider": "Memory",
    // or "Redis"
    "RedisConnectionString": "localhost:6379"
  }
}
```

### Registration

```csharp
services.AddCustomCaching(configuration);
```

## Benefits of Factory Method Pattern

1. **Loose Coupling** - Client code depends on abstractions (`ICacheFactory`, `ICacheProvider`) rather than concrete
   implementations
2. **Open/Closed Principle** - Easy to add new cache providers without modifying existing code
3. **Single Responsibility** - Object creation logic is separated from business logic
4. **Runtime Flexibility** - Cache provider can be switched via configuration without code changes

## Supported Providers

- ✅ **Memory** - In-memory caching (production-ready)
- ✅ **Redis** - Distributed caching (production-ready)
- ⏳ **DynamoDB** - Planned for future implementation

## Dependencies

- `StackExchange.Redis` - Redis client
- `Microsoft.Extensions.Caching.Memory` - In-memory caching
- `Microsoft.Extensions.Options` - Configuration binding
- `Microsoft.Extensions.DependencyInjection` - Dependency injection
