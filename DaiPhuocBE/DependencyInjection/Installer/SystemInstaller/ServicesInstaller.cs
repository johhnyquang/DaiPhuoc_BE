
using DaiPhuocBE.Services.AuthServices;

namespace DaiPhuocBE.DependencyInjection.Installer.SystemInstaller
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
