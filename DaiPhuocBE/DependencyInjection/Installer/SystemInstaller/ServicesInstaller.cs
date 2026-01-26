
using DaiPhuocBE.Services.AuthServices;
using DaiPhuocBE.Services.CustomerServices;
using DaiPhuocBE.Services.UserServices;

namespace DaiPhuocBE.DependencyInjection.Installer.SystemInstaller
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
