

using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Services;
using MedicalCoreAplications.cs.Services.systems;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalIOC.cs.Dependecy.Configurations
{
    public static class SystemsDependecy
    { 
        public static void AddSystemDependecy(this IServiceCollection services)
        {
           services.AddScoped<INotificationsRepository, NotificationsRepositories>();
           services.AddScoped<IStatusRepository, StatusRepositorie>(); 
           services.AddScoped<IRoleRepository, RoleRepositorie>();

            services.AddTransient<IStatusServices, StatusServices>();   
            services.AddTransient<INotificationServices, NotificationsServices>();
            services.AddTransient<IRoleServices, RoleServices>();
           
        }
    }
}
