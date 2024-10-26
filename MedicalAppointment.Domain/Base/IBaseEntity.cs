
namespace MedicalAppointment.Domain.Base
{
    public interface IBaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
