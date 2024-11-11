

using MedicalAppointment.Persistance.Interfaces;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalIOC.cs.Dependecy.Configurations
{
    public static class ConfigurationsDependecy
    { 

        public static void AddConfigurationDependecy(this IServiceCollection services)
        {
            services.AddScoped<UserInterfaces, UserRepositorie>(); 
            services.AddScoped<IDoctorInterfaces, DoctorRepositorie>();
            services.AddScoped<IPatientInterfaces, PatientRepositorie>();

            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IDoctorServices, Doctorservices>();
            services.AddTransient<IPatientsServices, PatientServices>();
        }
    }
}
