
using MedicalAppointment.Domain.Base;

namespace MedicalAppointment.Domain.Entities.Confi.Systems
{
    public sealed class SystemRole : BaseEntity
    { 
        public int  RoleId { get; set; }    
        public string RoleName { get; set; }
    }
}
