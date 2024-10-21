using MedicalAppointment.Domain.Entities.Insurance;
using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.Insurance
{
    public interface IInsuranceProvidersRepository : IBaseRepository<InsuranceProviders>
    {
        List<OperationResult> GetInsuranceProvidersById(int insuranceProvidersId);
    }
}
