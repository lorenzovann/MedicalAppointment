

using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.appointments
{
    public interface IDoctorAvailabilityRepository : IBaseRepository<DoctorAvailability>
    {
        List<OperationResult> GetDoctorAvailabilityById(int doctorAvailabilityId);
    }
}
