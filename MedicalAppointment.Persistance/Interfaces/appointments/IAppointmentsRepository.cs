

using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.appointments
{
    public interface IAppointmentsRepository : IBaseRepository<Appointments>
    {
        public List<OperationResult> GetAppointmentsByAppointmentId(int appointmentsId);
    }
}
