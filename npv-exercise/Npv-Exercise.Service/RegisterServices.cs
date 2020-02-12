using Microsoft.Extensions.DependencyInjection;
using Npv_Exercise.Service.Services.NPV;
using Npv_Exercise.Service.Services.NpvVariables;

namespace Npv_Exercise.Service
{
    public static class RegisterServices
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            services.AddScoped<INpvService, NpvService>();
            services.AddScoped<INpvVariableService, NpvVariableService>();
        }
    }
}
