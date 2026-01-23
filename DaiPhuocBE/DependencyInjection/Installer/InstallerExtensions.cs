
namespace DaiPhuocBE.DependencyInjection.Installer
{
    public static class InstallerExtensions 
    {
        public static void InstallerServiceInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes
                            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface  && !x.IsAbstract)
                            .Select(Activator.CreateInstance)
                            .Cast<IInstaller>().ToList();

            foreach (var installer in installers)
            {
                installer.InstallService(services, configuration);
            }
        }
    }
}
