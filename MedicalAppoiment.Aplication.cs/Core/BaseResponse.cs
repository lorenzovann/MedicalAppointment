

namespace MedicalAppoiment.Aplication.cs.Core
{
    public abstract class BaseResponse
    { 

        public bool Success { get; set; }
        public string? Menssage { get; set; }
        protected BaseResponse() { this.Success = true; }

    }
}
