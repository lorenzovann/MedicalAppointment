


using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Services.InsuranceService;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using MedicalAppointment.Persistance.Repositories.InsuranceRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalAppointment.IOC.InsuranceDependency
{
    public static class InsuranceDependency 
    {
        public static void AddInsuranceDependency(this IServiceCollection service)
        {

            service.AddScoped<IInsuranceProvidersRepository, InsuranceProvidersRepository>();

            service.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();

            service.AddTransient<IInsuranceProvidersService, InsuranceProvidersService>();

            service.AddTransient<INetworkTypeService, NetworkTypeService>();



        }
    }
}
