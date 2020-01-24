using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Uvic_Ecg_Model
{
    public class AppointmentMail
    {
        [JsonProperty] private string patientEmail;
        [JsonProperty] private string mailContent;
        public AppointmentMail(string address, string content)
        {
            PatientEmail = address;
            MailContent = content;
        }
        public string PatientEmail { get => patientEmail; set => patientEmail = value; }
        public string MailContent { get => mailContent; set => mailContent = value; }
    }
}
