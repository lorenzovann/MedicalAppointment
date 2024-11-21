

namespace MedicalAppointment.Domain.Result
{
    public class OperationResult
    {

        public bool Success { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }

    }
}
