using MedicalAppointment.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedicalAppointment.Domain.Entities.Configuration
{
    [Table("NetworkType", Schema = "Insurance")]
    
    public class NetworkType : BaseEntity
    {
        [Key]
        public int NetworkTypeID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        

    }
}
