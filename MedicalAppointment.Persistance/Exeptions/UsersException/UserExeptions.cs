using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Exeptions.UsersException
{
    public sealed class UserExeptions : Exception
    {
        public UserExeptions(string mensaje) : base(mensaje) { }


    }
}
