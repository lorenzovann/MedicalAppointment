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
            var appointmentsResponse = new AppointmentsResponse();

            try
            {
                var result = await _appointmentsRepository.GetAll();

                if (result.Data is List<Appointments> appointmentsList)
                {
                    appointmentsResponse.Data = appointmentsList
                                                .Select(appointment => new AppoinmentsGetDto
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

        public async Task<AppointmentsResponse> GetById(int Id)
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                var result = await _appointmentsRepository.GetEntityBy(Id);

                if (!result.Success)
                {
                    appointmentsResponse.Message = result.Message;
                    appointmentsResponse.IsSuccess = result.Success;
                    return appointmentsResponse;
                }

                appointmentsResponse.Data = result.Data;

            }
            catch (Exception ex)
            {

                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = "Error obteniendo los autobuses";
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
                    AppoinmentsGetDto getDto = new AppoinmentsGetDto
                    {
                        AppointmentID = appointments.AppointmentID,
                        PatientID = appointments.PatientID,
                        DoctorID = appointments.DoctorID,
                        AppointmentDate = appointments.AppointmentDate,
                        StatusID = appointments.StatusID
                    };

                    appointmentsResponse.Data = getDto;
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
                
                var resultGetId = await _appointmentsRepository.GetEntityBy(dto.AppointmentID);

               
                if (!resultGetId.Success)
                {
                    appointmentsResponse.IsSuccess = resultGetId.Success;
                    appointmentsResponse.Message = resultGetId.Message;
                    return appointmentsResponse;
                }

                
                Appointments appointments = new Appointments
                {
                    AppointmentID = dto.AppointmentID,
                    PatientID = dto.PatientID,
                    DoctorID = dto.DoctorID,
                    AppointmentDate = dto.AppointmentDate,
                    StatusID = dto.StatusID,
                    CreatedAt = DateTime.Now,  
                    UpdatedAt = DateTime.Now,  
                    IsActive = dto.IsActive   
                };

                
                var result = await _appointmentsRepository.Update(appointments);

                appointmentsResponse.IsSuccess = result.Success;
                appointmentsResponse.Message = result.Success ? "Cita actualizada exitosamente." : result.Message;
            }
            catch (Exception ex)
            {
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error {ex.Message} actualizando la cita.";
                _logger.LogError(appointmentsResponse.Message, ex);
            }

            return appointmentsResponse;
        }


    }

}

