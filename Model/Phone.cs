using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Uvic_Ecg_Model
{
    class Phone
    {
        [JsonProperty] private int phoneId;
        [JsonProperty] private string phoneName;
        [JsonProperty] private string phoneMac;
        [JsonProperty] private bool deleted;
        [JsonProperty] private int clinicId;
        [JsonProperty] private string model;
        public Phone(int phoneIdi,
                     string phoneNamei,
                     string phoneMaci,
                     bool deletedi,
                     int clinicIdi,
                     string modeli)
        {
            phoneId = phoneIdi;
            phoneName = phoneNamei;
            phoneMac = phoneMaci;
            deleted = deletedi;
            clinicId = clinicIdi;
            model = modeli;
        }
        public int PhoneId { get => phoneId; set => phoneId = value; }
        public string PhoneName { get => phoneName; set => phoneName = value; }
        public string PhoneMac { get => phoneMac; set => phoneMac = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public string Model { get => model; set => model = value; }
    }
}
