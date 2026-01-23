
using DaiPhuocBE.Repositories;
using DaiPhuocBE.Repositories.UserRepository;

namespace DaiPhuocBE.DependencyInjection.Installer.SystemInstaller
{
    public class UnitOfWorkInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
