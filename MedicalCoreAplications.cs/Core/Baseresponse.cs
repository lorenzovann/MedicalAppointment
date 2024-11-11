
namespace MedicalCoreAplications.cs.Core
{
    public abstract class Baseresponse
    {

   
        public string? Menssaje { get; set; }
        public  bool  success { get; set; }
        public Baseresponse() { this.success = true; }



    }
}
