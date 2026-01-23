namespace DaiPhuocBE.DependencyInjection.Installer
{
    public interface IInstaller
    {
        void InstallService(IServiceCollection services, IConfiguration configurations);
    }
}
