using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Exeptions
{
    public sealed class Exeptions : Exception
    { 
        public Exeptions(string mensaje) : base (mensaje) { }   


    }
}
