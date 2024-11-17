

namespace MedicalAppointment.Persistance.Model.Systems
{
    public class PatientModel
    {
        public int PatientID { get; set; }
        public string NamePatient { get; set; }
        public DateTime DateofBirth { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
    }
}
