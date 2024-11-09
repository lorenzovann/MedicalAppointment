using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dto.Dtosappointments.Appointments;
using MedicalAppointment.Application.Responses.appointmentsResponses;
using MedicalAppointment.Application.Responses.InsuranceResponses;

namespace MedicalAppointment.Application.Contracts.appointmentsContracts
{
    public interface IAppointmentsService : IBaseService<AppointmentsResponse, AppointmentSaveDto, AppointmentUpdateDto>
    {

    }
}
