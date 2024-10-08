
using MedicalAppointment.Domain.Entities.Confi.Systems;
using MedicalAppointment.Domain.Entities.Confi.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Medical.Percistances.cs.Context
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            #region confi Entities
            #endregion

        }
        // Mapeo de las entidades

        #region " Entities User "
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> UserDoctor { get; set; } 
        public DbSet<Patient> userPatients { get; set; }
        #endregion 


        #region #region " Entities System " 

        public DbSet<SystemStatus> systemStatus { get; set; }
        public DbSet<SystemNotifications> systemNotifications { get; set; }
        public DbSet<SystemRole> systemRoles { get; set; }
        #endregion 


    }

}  