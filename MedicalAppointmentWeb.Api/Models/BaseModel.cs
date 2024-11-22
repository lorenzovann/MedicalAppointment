namespace MedicalAppointmentWeb.Api.Models
{
    public abstract class BaseModel
    {
        public bool isSuccess { get; set; }
        public object message { get; set; }
    }
}
