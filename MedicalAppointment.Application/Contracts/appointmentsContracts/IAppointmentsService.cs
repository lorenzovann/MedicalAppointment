using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dto.Dtosappointments.Appointments;
using MedicalAppointment.Application.Dto.Dtosappointments.AppointmentsDtos;
using MedicalAppointment.Application.Responses.appointmentsResponses;


namespace MedicalAppointment.Application.Contracts.appointmentsContracts
{
    public interface IAppointmentsService : IBaseService<AppointmentsResponse, AppoinmentsGetDto, AppointmentsSaveDto, AppointmentsUpdateDto>
    {

    }
}
