

namespace MedicalAppointment.Domain.Result
{
    public class OperationResult
    {

        public OperationResult() { this.Sucess = true; }

        public bool? Sucess { get; set; }

        public string Message { get; set; }

        public dynamic? data { get; set; }




    }
}
