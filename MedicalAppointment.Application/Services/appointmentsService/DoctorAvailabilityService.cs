
using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos;
using MedicalAppointment.Application.Responses.appointmentsResponses;
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
            throw new NotImplementedException();
        }

        public async Task<DoctorAvailabilityResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorAvailabilityResponse> SaveAsync(DoctorAvailabilitySaveDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorAvailabilityResponse> UpdateAsync(DoctorAvailabilityUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
