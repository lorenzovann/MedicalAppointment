using MedicalAppointment.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedicalAppointment.Domain.Entities.Insurance
{
    [Table("NetworkType", Schema = "Insurance")]

    public class NetworkType : BaseEntity , IBaseEntityForTwo
    {
        [Key]
        public int NetworkTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        


    }
}
