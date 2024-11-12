


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



        }
    }
}
