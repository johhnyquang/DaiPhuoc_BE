
using DaiPhuocBE.DependencyInjection.Options;
using DaiPhuocBE.Services.CacheServices;
using StackExchange.Redis;

namespace DaiPhuocBE.DependencyInjection.Installer.SystemInstaller
{
    public class RedisInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            services.Configure<RedisSettings>(configurations.GetSection(nameof(RedisSettings)));
            var redisConfig = configurations.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

            if (!redisConfig.Enabled)
            {
                // Nếu Redis disable,dùng In-memory cache
                services.AddMemoryCache();
                services.AddSingleton<ICacheService, InMemoryCacheService>();
                return;
            }

            var redisConfigurationOptions = ConfigurationOptions.Parse(redisConfig.ConnectionString);
            // Config Redis with production enviroment
            if (redisConfig.EnviromentEnabled)
            {
                redisConfigurationOptions.AbortOnConnectFail = false; // Không crash khi Redis down
                redisConfigurationOptions.ConnectTimeout = 5000;
                redisConfigurationOptions.SyncTimeout = 5000;
                redisConfigurationOptions.ConnectRetry = 3;
                redisConfigurationOptions.KeepAlive = 60;
            }

            // Logging
            //var logger = services.BuildServiceProvider().GetRequiredService<ILogger<RedisInstaller>>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RedisInstaller>>();

                var connection = ConnectionMultiplexer.Connect(redisConfigurationOptions);

                // Log connection events
                connection.ConnectionFailed += (sender, args) =>
                {
                    logger.LogError("Redis connection failed: {Exception}", args.Exception?.Message);
                };

                connection.ConnectionRestored += (sender, args) =>
                {
                    logger.LogInformation("Redis connection restored");
                };

                connection.ErrorMessage += (sender, args) =>
                {
                    logger.LogError("Redis error: {Message}", args.Message);
                };

                logger.LogInformation("Redis connected successfully to {Endpoints}",string.Join(", ", connection.GetEndPoints().ToString()));

                return connection;
            });

            services.AddSingleton<ICacheService, RedisCacheService>();
        }
    }
}
