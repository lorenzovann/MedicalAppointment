

using MedicalAppointment.Persistance.Interfaces;
using MedicalAppointment.Persistance.Interfaces.Configurations;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Services.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalIOC.cs.Dependecy.Configurations
{
    public static class ConfigurationsDependecy
    { 

        public static void AddConfigurationDependecy(this IServiceCollection services)
        {
           
            services.AddScoped<IDoctorRepository, DoctorRepositorie>();
            services.AddScoped<IPatientRepository,  PatientRepositorie>();
            services.AddScoped<IUserRepository, UserRepositorie>();

            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IDoctorServices, DoctorServices>();
            services.AddTransient<IPatientServices, PatientServices>();
        }
    }
}
