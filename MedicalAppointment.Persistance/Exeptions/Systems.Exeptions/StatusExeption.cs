using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Exeptions.Systems.Exeptions
{
    public class StatusExeption : Exception
    { 

        public StatusExeption(string mensaje) :base(mensaje) { } 

    }
}
