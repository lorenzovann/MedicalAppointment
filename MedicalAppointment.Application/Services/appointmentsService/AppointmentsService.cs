using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Core;
using MedicalAppointment.Application.Dto.Dtosappointments.Appointments;
using MedicalAppointment.Application.Dto.Dtosappointments.AppointmentsDtos;
using MedicalAppointment.Application.Responses.appointmentsResponses;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Interfaces.appointments;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.appointmentsService
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly ILogger<AppointmentsService> _logger;

        public AppointmentsService(IAppointmentsRepository appointmentsRepository,
                                   ILogger<AppointmentsService> logger)
        {
            if (appointmentsRepository is null)
            {
                throw new ArgumentNullException(nameof(appointmentsRepository));
            }

            _appointmentsRepository = appointmentsRepository;
            _logger = logger;

        }

        public async Task<AppointmentsResponse> GetAll()
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                var result = await _appointmentsRepository.GetAll();

                if (result.Data is List<Appointments> appointmentsList)
                {
                    appointmentsResponse.Data = appointmentsList
                                                .Select(appointment => new Appointments
                                                {
                                                    AppointmentID = appointment.AppointmentID,
                                                    PatientID = appointment.PatientID,
                                                    DoctorID = appointment.DoctorID,
                                                    AppointmentDate = appointment.AppointmentDate,
                                                    StatusID = appointment.StatusID
                                                }).ToList();

                    appointmentsResponse.IsSuccess = true;
                    appointmentsResponse.Message = "Listado de Appointments obtenido con éxito.";
                }
                else
                {
                    appointmentsResponse.IsSuccess = false;
                    appointmentsResponse.Message = "No se encontraron Appointments.";
                }
            }
            catch (Exception ex)
            {
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error {ex.Message} tratando de listar Appointments.";
                _logger.LogError(appointmentsResponse.Message, ex.ToString());
            }
            return appointmentsResponse;
        }

        public async Task<AppointmentsResponse> GetById(int id)
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                var result = await _appointmentsRepository.GetEntityBy(id);

                if (!result.Success)
                {
                    appointmentsResponse.IsSuccess = result.Success;
                    appointmentsResponse.Message = result.Message;
                    return appointmentsResponse;
                }

                appointmentsResponse.Data = result.Data;

            }
            catch (Exception ex)
            {

                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error {ex.Message} obteniendo Appointments";
                _logger.LogError(appointmentsResponse.Message, ex.ToString());

            }

            return appointmentsResponse;
        }








        public async Task<AppointmentsResponse> SaveAsync(AppointmentsSaveDto dto)
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                Appointments appointments = new Appointments
                {

                    PatientID = dto.PatientID,
                    DoctorID = dto.DoctorID,
                    AppointmentDate = dto.AppointmentDate,
                    StatusID = dto.StatusID
                };


                var saveResult = await _appointmentsRepository.Save(appointments);

                if (saveResult.Success)
                {
                    AppointmentsSaveDto saveDto = new AppointmentsSaveDto
                    {

                        PatientID = appointments.PatientID,
                        DoctorID = appointments.DoctorID,
                        AppointmentDate = appointments.AppointmentDate,
                        StatusID = appointments.StatusID
                    };

                    appointmentsResponse.Data = saveDto;
                    appointmentsResponse.IsSuccess = true;
                    appointmentsResponse.Message = "Appointments guardado exitosamente.";
                }
                else
                {
                    appointmentsResponse.IsSuccess = false;
                    appointmentsResponse.Message = saveResult.Message;
                }
            }
            catch (Exception ex)
            {
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error {ex.Message} tratando de guardar Appointments.";
                _logger.LogError(appointmentsResponse.Message, ex.ToString());
            }
            return appointmentsResponse;
        }

        public async Task<AppointmentsResponse> UpdateAsync(AppointmentsUpdateDto dto)
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                var resultGetById = await _appointmentsRepository.GetEntityBy(dto.AppointmentID);

                if (!resultGetById.Success)
                {
                    appointmentsResponse.IsSuccess = resultGetById.Success;
                    appointmentsResponse.Message = resultGetById.Message;

                    return appointmentsResponse;
                }

                Appointments? appointment = new Appointments();

                appointment.AppointmentID = dto.AppointmentID;
                appointment.PatientID = dto.PatientID;
                appointment.DoctorID = dto.DoctorID;
                appointment.AppointmentDate = dto.AppointmentDate;
                appointment.StatusID = dto.StatusID;
                appointment.UpdatedAt = dto.UpdatedAt;

                var result = await _appointmentsRepository.Update(appointment);

            }
            catch (Exception ex)
            {
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = "Error al actualizar el appointment";
                _logger.LogError(appointmentsResponse.Message, ex.ToString());

            }
            return appointmentsResponse;





        }

    }
}

