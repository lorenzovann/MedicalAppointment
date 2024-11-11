

using MedicalAppoiment.Infraectructure.Interfaces;

namespace MedicalAppoiment.Infraectructure.Model
{
    public class EmailModel 
    {
        public string? To { get; set; }
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
