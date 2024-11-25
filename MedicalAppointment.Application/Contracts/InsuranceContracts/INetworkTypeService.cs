

using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dto.DtosInsurance.NetworkTypeDtos;
using MedicalAppointment.Application.Responses.InsuranceResponses;

namespace MedicalAppointment.Application.Contracts.InsuranceContracts
{
    public interface INetworkTypeService : IBaseService<NetworkTypeResponse, NetworkTypeSaveDto, NetworkTypeUpdateDto>
    {
    }
}
