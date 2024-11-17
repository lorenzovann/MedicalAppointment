using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Domain.IBaseRepositorie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Interfaces.Configurations
{
    public interface IDoctorRepository : IBaseRepositorie<Doctor>
    {
    }
}
