

using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dto.DtosInsurance.InsuranceProvidersDtos;
using MedicalAppointment.Application.Responses.InsuranceResponses;

namespace MedicalAppointment.Application.Contracts.InsuranceContracts
{
    public interface IInsuranceProvidersService : IBaseService<InsuranceProvidersResponse, InsuranceProvidersSaveDto, InsuranceProvidersUpdateDto>
    {

    }
}
