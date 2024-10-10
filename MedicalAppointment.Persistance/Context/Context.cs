
using Medical.Domain.Entities.Confi.Systems;
using Medical.Domain.Entities.Confi.Users;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Medical.Percistances.cs.Context
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
         


        }
        // Mapeo de las entidades

        #region " Entities Users "
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> UserDoctor { get; set; } 
        public DbSet<Patient> userPatients { get; set; }
        #endregion 


        #region  " Entities Systems " 

        public DbSet<SystemStatus> systemStatus { get; set; }
        public DbSet<SystemNotifications> systemNotifications { get; set; }
        public DbSet<SystemRole> systemRoles { get; set; }
        #endregion 


    }

}  