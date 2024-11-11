
namespace MedicalCoreAplications.cs.Core
{
    public abstract class Baseresponse
    {

        public Baseresponse() { this.success = true; }
        public string? Menssaje { get; set; }
        public  bool  success { get; set; }
       


    }
}
