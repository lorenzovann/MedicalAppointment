
using Medical.Domain.Base;


namespace MedicalAppointment.Domain.Entities.Confi.Systems
{ 
    [Table("Role", Schema = "System")]
    public sealed class SystemRole : BaseEntitie
    { 
        [Key]
        public int  RoleId { get; set; }    
        public string RoleName { get; set; }
    }
}
       
