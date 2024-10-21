using MedicalAppointment.Domain.Entities.Insurance;
using MedicalAppointment.Domain.Entities.appointments;
using Microsoft.EntityFrameworkCore;


namespace MedicalAppointment.Persistance.Context
{
    public partial class MedicalAppointmentContext : DbContext
    {
        public MedicalAppointmentContext(DbContextOptions<MedicalAppointmentContext> options) : base(options)
        {

        }

        #region "appointments Entities"
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        
        #endregion

        
        
        #region "Insurance entities"

        public DbSet<InsuranceProviders> InsuranceProviders { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }

        #endregion

    }
}
