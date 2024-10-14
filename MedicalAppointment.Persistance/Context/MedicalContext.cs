
using Medical.Domain.Entities.Confi.Systems;
using Medical.Domain.Entities.Confi.Users;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Medical.Percistances.cs.Context
{
    public partial class MedicalContext : DbContext
    {
        public MedicalContext(DbContextOptions<MedicalContext> options) : base(options)
        {



        }
        // Mapeo de las entidades

        #region " Entities Users "
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        #endregion 


        #region  " Entities Systems " 

        public DbSet<Status> Status { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Role>  Roles { get; set; }
        #endregion 


    }

}  