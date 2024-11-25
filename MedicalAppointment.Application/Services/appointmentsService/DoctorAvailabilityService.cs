
using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.AppointmentsDtos;
using MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos;
using MedicalAppointment.Application.Responses.appointmentsResponses;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Interfaces.appointments;
using MedicalAppointment.Persistance.Repositories.appointmentsRepositories;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.appointmentsService
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly ILogger<DoctorAvailabilityService> _logger; 

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository, 
                                         ILogger<DoctorAvailabilityService> logger)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _logger = logger;
        }

        public async Task<DoctorAvailabilityResponse> GetAll()
        {
            DoctorAvailabilityResponse doctorAvailabilityResponse = new DoctorAvailabilityResponse();

            try
            {
                var result = await _doctorAvailabilityRepository.GetAll();

                if (result.Data is List<DoctorAvailability> doctorAvailabilityList)
                {
                    doctorAvailabilityResponse.Data = doctorAvailabilityList
                                                .Select(doctorAvailability => new DoctorAvailability
                                                {
                                                    AvailabilityID = doctorAvailability.AvailabilityID,
                                                    DoctorID = doctorAvailability.DoctorID,
                                                    AvailableDate = doctorAvailability.AvailableDate,
                                                    StartTime = doctorAvailability.StartTime,
                                                    EndTime = doctorAvailability.EndTime,

                                                }).ToList();

                    doctorAvailabilityResponse.IsSuccess = true;
                    doctorAvailabilityResponse.Message = "Listado de Appointments obtenido con éxito.";
                }
                else
                {
                    doctorAvailabilityResponse.IsSuccess = false;
                    doctorAvailabilityResponse.Message = "No se encontraron Appointments.";
                }
            }
            catch (Exception ex)
            {
                doctorAvailabilityResponse.IsSuccess = false;
                doctorAvailabilityResponse.Message = $"Error {ex.Message} tratando de listar Appointments.";
                _logger.LogError(doctorAvailabilityResponse.Message, ex.ToString());
            }
            return doctorAvailabilityResponse;
        }

        public async Task<DoctorAvailabilityResponse> GetById(int id)
        {
            DoctorAvailabilityResponse doctorAvailabilityResponse = new DoctorAvailabilityResponse();

            try
            {
                var result = await _doctorAvailabilityRepository.GetEntityBy(id);

                if (!result.Success)
                {
                    doctorAvailabilityResponse.Message = result.Message;
                    doctorAvailabilityResponse.IsSuccess = result.Success;
                    return doctorAvailabilityResponse;
                }

                doctorAvailabilityResponse.Data = result.Data;

            }
            catch (Exception ex)
            {

                doctorAvailabilityResponse.IsSuccess = false;
                doctorAvailabilityResponse.Message = "Error obteniendo DoctorAvailability";
                _logger.LogError(doctorAvailabilityResponse.Message, ex.ToString());

            }

            return doctorAvailabilityResponse;
        }

        public async Task<DoctorAvailabilityResponse> SaveAsync(DoctorAvailabilitySaveDto dto)
        {
            DoctorAvailabilityResponse doctorAvailabilityResponse = new DoctorAvailabilityResponse();

            try
            {
                DoctorAvailability doctorAvailability = new DoctorAvailability
                {
                  
                    DoctorID = dto.DoctorID,
                    AvailableDate = dto.AvailableDate,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                };

                var saveResult = await _doctorAvailabilityRepository.Save(doctorAvailability);

                if (saveResult.Success)
                {
                    DoctorAvailabilitySaveDto getDto = new DoctorAvailabilitySaveDto
                    {
                        
                        DoctorID = doctorAvailability.DoctorID,
                        AvailableDate = doctorAvailability.AvailableDate,
                        StartTime = doctorAvailability.StartTime,
                        EndTime = doctorAvailability.EndTime,
                    };

                    doctorAvailabilityResponse.Data = getDto;
                    doctorAvailabilityResponse.IsSuccess = true;
                    doctorAvailabilityResponse.Message = "DoctorAvailability guardado exitosamente.";
                }
                else
                {
                    doctorAvailabilityResponse.IsSuccess = false;
                    doctorAvailabilityResponse.Message = saveResult.Message;
                }
            }
            catch (Exception ex)
            {
                doctorAvailabilityResponse.IsSuccess = false;
                doctorAvailabilityResponse.Message = $"Error {ex.Message} tratando de guardar DoctorAvailability.";
                _logger.LogError(doctorAvailabilityResponse.Message, ex.ToString());
            }

            return doctorAvailabilityResponse;
        }

        public async Task<DoctorAvailabilityResponse> UpdateAsync(DoctorAvailabilityUpdateDto dto)
        {
            DoctorAvailabilityResponse doctorAvailabilityResponse = new DoctorAvailabilityResponse();

            try
            {

                var resultGetId = await _doctorAvailabilityRepository.GetEntityBy(dto.AvailabilityID);

                if (!resultGetId.Success)
                {
                    doctorAvailabilityResponse.IsSuccess = false;
                    doctorAvailabilityResponse.Message = resultGetId.Message;
                    return doctorAvailabilityResponse;
                }


                DoctorAvailability? doctorAvailability = (DoctorAvailability)resultGetId.Data!;

                doctorAvailability.AvailabilityID = dto.AvailabilityID;
                doctorAvailability.DoctorID = dto.DoctorID;
                doctorAvailability.AvailableDate = dto.AvailableDate;
                doctorAvailability.StartTime = dto.StartTime;   
                doctorAvailability.EndTime = dto.EndTime;


                var result = await _doctorAvailabilityRepository.Update(doctorAvailability);

                doctorAvailabilityResponse.IsSuccess = result.Success;
                doctorAvailabilityResponse.Data = result.Success ? doctorAvailability : null;
                doctorAvailabilityResponse.Message = result.Success ? "DoctorAvailability actualizado exitosamente." : result.Message;
            }
            catch (Exception ex)
            {
                doctorAvailabilityResponse.IsSuccess = false;
                doctorAvailabilityResponse.Message = $"Error actualizando DoctorAvailability: {ex.Message}";
                _logger.LogError(doctorAvailabilityResponse.Message, ex.ToString());
            }

            return doctorAvailabilityResponse;
        }
    }
}
