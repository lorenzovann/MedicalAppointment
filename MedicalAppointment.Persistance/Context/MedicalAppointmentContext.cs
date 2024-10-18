using MedicalAppointment.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Context
{
    public partial class MedicalAppointmentContext : DbContext
    {
        public MedicalAppointmentContext(DbContextOptions<MedicalAppointmentContext> options) : base(options)
        {

        }

        #region "System Entities"
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        public DbSet<InsuranceProviders> InsuranceProviders { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }
        #endregion



    }
}
