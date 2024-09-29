using MedicalAppointment.Domain.Base;


namespace MedicalAppointment.Domain.Entities.Configuration
{
    public class NetworkType : BaseEntity
    {
        public int NetworkTypeID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

    }
}
