
using DaiPhuocBE.Data;
using DaiPhuocBE.DependencyInjection.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DaiPhuocBE.DependencyInjection.Installer.SystemInstaller
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            
            services.Configure<DatabaseSettings>(configurations.GetSection(nameof(DatabaseSettings)));
            var dbConfig = configurations.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

            if (dbConfig == null)
            {
                throw new InvalidOperationException($"Configuration section {nameof(DatabaseSettings)} is missing");
            }

            if (string.IsNullOrEmpty(dbConfig.ConnectionStrings))
            {
                throw new InvalidOperationException($"ConnectionStrings cannot empty");
            }

            services.AddDbContext<MasterDbContext>(options =>
            {
                options.UseOracle(dbConfig.ConnectionStrings, sqlOptions =>
                {
                    sqlOptions.UseOracleSQLCompatibility("11");
                    sqlOptions.CommandTimeout(dbConfig.CommandTimeout);
                }).ReplaceService<IModelCacheKeyFactory, SchemaCacheKeyFactory>()
                .LogTo(Console.WriteLine, LogLevel.Information); // in câu sql ra console
            });

            services.AddScoped<Func<string, MasterDbContext>>(provider =>
            {
                var options = provider.GetRequiredService<DbContextOptions<MasterDbContext>>();
                string schemaName = configurations.GetSection("SchemaName").Value!.ToString();
                return (schema) => new MasterDbContext(options, schemaName, schema);
            });
        }
    }
}
