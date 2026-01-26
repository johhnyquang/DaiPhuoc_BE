
using Microsoft.Extensions.Caching.Memory;

namespace DaiPhuocBE.Services.CacheServices
{
    public class InMemoryCacheService (IMemoryCache memoryCache, ILogger<InMemoryCacheService> logger) : ICacheService
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly ILogger<InMemoryCacheService> _logger = logger;

        public Task ClearAllAsync()
        {
            if (_memoryCache is MemoryCache memCache)
            {
                memCache.Compact(1.0);
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string key)
        {
            return Task.FromResult(_memoryCache.TryGetValue(key, out _));
        }

        public Task<T> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);   
            return Task.FromResult(value);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(60)
            };

            _memoryCache.Set(key, value, cacheOptions);
            return Task.FromResult(value);
        }
    }
}
