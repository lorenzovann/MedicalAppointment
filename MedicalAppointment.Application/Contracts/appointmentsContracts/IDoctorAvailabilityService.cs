

using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos;
using MedicalAppointment.Application.Responses.appointmentsResponses;

namespace MedicalAppointment.Application.Contracts.appointmentsContracts
{
    public interface IDoctorAvailabilityService : IBaseService<DoctorAvailabilityResponse,DoctorAvailabilityGetDto, DoctorAvailabilitySaveDto, DoctorAvailabilityUpdateDto>
    {
    }
}
