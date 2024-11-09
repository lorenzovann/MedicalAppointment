using MedicalAppointment.Application.Contracts.appointmentsContracts;
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
                
                List<AppoinmentsGetDto> appoinments = ((List<Appointments>)result.Data)
                                                        .Select(appoinments => new AppoinmentsGetDto
                                                        {
                                                            AppointmentID = appoinments.AppointmentID,
                                                            PatientID = appoinments.PatientID,
                                                            DoctorID = appoinments.DoctorID,
                                                            AppointmentDate = appoinments.AppointmentDate,
                                                            StatusID = appoinments.StatusID

                                                        }).ToList();

                appointmentsResponse.IsSuccess = result.Data;
                appointmentsResponse.Message = result.Message;

            }

            catch (Exception ex)
            {
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error tipo {ex.Message} tratando de listar Appointments.";

            }
            return appointmentsResponse;

        }

        public async Task<AppointmentsResponse> GetById(int id)
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                var result = await _appointmentsRepository.GetEntityBy(id); 
                if (result.Data != null)
                {
                    Appointments appointments = (Appointments)result.Data;

                    AppoinmentsGetDto getDto = new AppoinmentsGetDto()
                    {
                        AppointmentID = appointments.AppointmentID,
                        PatientID = appointments.PatientID,
                        DoctorID = appointments.DoctorID,
                        AppointmentDate = appointments.AppointmentDate,
                        StatusID = appointments.StatusID
                    };

                    appointmentsResponse.Data = getDto;
                    appointmentsResponse.IsSuccess = true;  // Asignar el estado de éxito.
                    appointmentsResponse.Message = "Appointments encontrado con éxito.";
                }
                else
                {
                    appointmentsResponse.IsSuccess = false;
                    appointmentsResponse.Message = "Appointments no encontrado.";
                }
            }
            catch (Exception ex)
            {
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error {ex.Message} al obtener Appointments por su Id {id}.";
                _logger.LogError(appointmentsResponse.Message, ex);
            }

            return appointmentsResponse;
        }


        public async Task<AppointmentsResponse> SaveAsync(AppointmentSaveDto dto)
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                Appointments appointments = new Appointments();

                appointments.PatientID = dto.PatientID;
                appointments.DoctorID = dto.DoctorID;
                appointments.AppointmentDate = dto.AppointmentDate;
                appointments.StatusID = dto.StatusID;


                appointmentsResponse.Data = await _appointmentsRepository.Save(appointments);
                appointmentsResponse.Message = "Appointments guardado exitosamente.";



            }

            catch (Exception ex) 
            { 
               
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error {ex.Message} tratando de guardar Appointments.";
                _logger.LogError(appointmentsResponse.Message, ex.ToString());
                               
            }
            return appointmentsResponse;
            
        }

        public async Task<AppointmentsResponse> UpdateAsync(AppointmentUpdateDto dto)
        {
            AppointmentsResponse appointmentsResponse = new AppointmentsResponse();

            try
            {
                var result = await _appointmentsRepository.GetEntityBy(dto.AppointmentID);

                if (result.Data != null) 
                { 
                    Appointments appointments = (Appointments)result.Data;

                    appointments.AppointmentID = dto.AppointmentID;
                    appointments.AppointmentDate = dto.AppointmentDate;
                    appointments.StatusID= dto.StatusID;
                    appointments.IsActive = dto.IsActive ?? false;

                    var updateResult = await _appointmentsRepository.Update(appointments);
                    appointmentsResponse= updateResult.Data;
                    appointmentsResponse.Message = "Appointments actualizado exitosamente.";

                                   
                }

                else
                {
                    appointmentsResponse.IsSuccess=false;
                    appointmentsResponse.Message = "Appointments no encontrado.";
                }


            }
            catch (Exception ex)
            {
                appointmentsResponse.IsSuccess = false;
                appointmentsResponse.Message = $"Error {ex.Message} tratando de actualizar Appointments.";
                _logger.LogError(appointmentsResponse.Message, ex.ToString());
            }
            return appointmentsResponse;

        }
    }
}
