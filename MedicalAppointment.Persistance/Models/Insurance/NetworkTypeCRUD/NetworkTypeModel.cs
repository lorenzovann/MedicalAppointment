

namespace MedicalAppointment.Persistance.Models.Insurance.NetworkTypeCRUD
{
    public class NetworkTypeModel
    {
        public int NetworkTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsActive { get; set; }
        
    }
}
