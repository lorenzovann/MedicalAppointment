

using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.systems;
using MedicalCoreAplications.cs.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalCore.IOC.cs.Dependecy.Configurations
{
    public static class SystemsDependecy
    { 
        public static void AddSystemsDependecy(this IServiceCollection services)
        {
            services.AddScoped<INotificationsinterfaces, NotificationsRepositories>();
            services.AddScoped<IRoleInterfaces, RoleRepositorie>();
            services.AddScoped<IStatusInterfaces, StatusRepositorie>();


            services.AddTransient<INotificationsServices, NotificationsServices>();
            services.AddTransient<IRoleServices, RoleServices>();
            services.AddTransient<IStatusServices, StatusServices>();

        }
    }
}
