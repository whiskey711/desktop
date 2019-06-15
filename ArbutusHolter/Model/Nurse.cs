using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbutusHolter.Model
{
    public class Nurse
    {
        public int nurseId { get; set; }
        public string nurseLastName { get; set; }
        public string nurseMidName { get; set; }
        public string nurseFirstName { get; set; }
        public string nursePhoneNumber { get; set; }
        public string nurseEmail { get; set; }
        public int clinicId { get; set; }
        public string password { get; set; }
        public bool deleted { get; set; }

        public Nurse(int NurseId,
                     string NurseLastName,
                     string NurseMidName,
                     string NurseFirstName,
                     string NursePhoneNumber,
                     string NurseEmail,
                     int ClinicId,
                     string Password,
                     bool Deleted)
        {
            nurseId = NurseId;
            nurseLastName = NurseLastName;
            nurseMidName = NurseMidName;
            nurseFirstName = NurseFirstName;
            nurseMidName = NurseMidName;
            nursePhoneNumber = NursePhoneNumber;
            nurseEmail = NurseEmail;
            clinicId = ClinicId;
            password = Password;
            deleted = Deleted;
        }
    }
}
