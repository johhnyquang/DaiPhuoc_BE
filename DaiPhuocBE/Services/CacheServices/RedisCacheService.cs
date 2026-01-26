
using DaiPhuocBE.DependencyInjection.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Runtime;
using System.Text.Json;

namespace DaiPhuocBE.Services.CacheServices
{
    public class RedisCacheService (IConnectionMultiplexer redis, IOptions<RedisSettings> redisSettings, ILogger<RedisCacheService> logger) : ICacheService
    {
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly IDatabase _dbInstance = redis.GetDatabase(); // tên của của instance trong redis
        private readonly RedisSettings _redisSettings = redisSettings.Value;
        private readonly ILogger<RedisCacheService> _logger = logger;
        private string GetPrefixedKey(string key)
        {
            return $"{_redisSettings.InstanceName}{key}";
        }

        public async Task ClearAllAsync()
        {
            try
            {
                var server = _redis.GetServer(_redis.GetEndPoints().First());
                await server.FlushDatabaseAsync();
                _logger.LogWarning("All cache cleared!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing all cache");
            }
        }

        public async Task<bool> ExistsAsync(string key)
        {
            try
            {
                var prefixedKey = GetPrefixedKey(key);
                _logger.LogInformation("Cache hit for key {key}", key);
                return await _dbInstance.KeyExistsAsync(prefixedKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking cache existence for key: {Key}", key);
                return false;
            }
        }

        public async Task<T> GetAsync<T>(string key)
        {
            try
            {
                var prefixKey = GetPrefixedKey(key);
                var value = await _dbInstance.StringGetAsync(prefixKey);

                if (value.IsNullOrEmpty)
                {
                    _logger.LogWarning("Cache miss for key {key}", key);
                    return default!;
                }

                _logger.LogInformation("Cache hit for key {key}", key);
                return JsonSerializer.Deserialize<T>(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cache for key: {Key}", key);
                return default! ;
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                var prefixedKey = GetPrefixedKey(key);
                await _dbInstance.KeyDeleteAsync(prefixedKey);
                _logger.LogInformation("Cache removed for key: {Key}", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache for key: {Key}", key);
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            try
            {
                var prefixKey = GetPrefixedKey(key);
                var serializedValue = JsonSerializer.Serialize(value);

                var expirationTime = expiration ?? TimeSpan.FromMinutes(_redisSettings.DefaultExpirationMinutes);
                await _dbInstance.StringSetAsync(prefixKey, serializedValue, expirationTime);

                _logger.LogInformation("Cache set for key: {Key}, expiration: {Expiration}", key, expirationTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting cache for key: {Key}", key);
            }
        }
    }
}
