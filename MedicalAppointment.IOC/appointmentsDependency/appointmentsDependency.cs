

using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Services.appointmentsService;
using MedicalAppointment.Persistance.Interfaces.appointments;
using MedicalAppointment.Persistance.Repositories.appointmentsRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalAppointment.IOC.appointmentsDependency
{
    public static class appointmentsDependency
    {
        public static void AddappointmentsDependency(this IServiceCollection service)
        {
            service.AddScoped<IAppointmentsRepository, AppointmentsRepository>();

            service.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();   

            service.AddTransient<IAppointmentsService,AppointmentsService>();

            service.AddTransient<IDoctorAvailabilityService, DoctorAvailabilityService>();
        }
    }
}
