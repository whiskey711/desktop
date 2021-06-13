using System;
using Newtonsoft.Json;

namespace Uvic_Ecg_Model
{
    public class Reports
    {
        [JsonProperty] public int reportId { get; set; }
        [JsonProperty] public PatientInfo patientInfo { get; set; }
        [JsonProperty] public EcgTest ecgTest { get; set; }
        [JsonProperty] public string report { get; set; }

        public Reports(PatientInfo p, EcgTest e)
        {
            patientInfo = p;
            ecgTest = e;
        }
    }
}