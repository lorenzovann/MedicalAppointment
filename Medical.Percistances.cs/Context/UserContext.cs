
using MedicalAppointment.Domain.Entities.Confi.Systems;
using MedicalAppointment.Domain.Entities.Confi.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Medical.Percistances.cs.Context
{
    public partial class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            #region confi Entities
            #endregion

        }
        // Mapeo de las entidades

        #region " Entities User "
        public DbSet<User> Users { get; set; }
        public DbSet<UserDoctor> UserDoctor { get; set; }
        public DbSet<UserPatient> userPatients { get; set; }
        #endregion

        #region " Entities System " 

        public DbSet<SystemStatus> systemStatus { get; set; }
        public DbSet<SystemNotifications> systemNotifications { get; set; }
        public DbSet<SystemRole> systemRoles { get; set; }
        #endregion 

    }

}   

