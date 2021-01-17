using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uvic_Ecg_ArbutusHolter
{
    class Config
    {
        private static int clinicId = 1;
        public static int ClinicId { get => clinicId; }
        public enum Gender
        {
            Male,
            Female,
            Other
        }
    }
}
